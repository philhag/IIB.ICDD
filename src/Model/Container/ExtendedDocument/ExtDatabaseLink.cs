using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DatabaseSchemaReader;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Model.PayloadTriples;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using TCode.r2rml4net;
using TCode.r2rml4net.Mapping.Fluent;
using TCode.r2rml4net.RDB.DatabaseSchemaReader;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace IIB.ICDD.Model.Container.ExtendedDocument
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  ExtDatabaseLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class ExtDatabaseLink : CtExternalDocument
    {
        public string DbConnectionString
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.ExtDbConnectionString);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.ExtDbConnectionString, value, AttributeType.String);
        }
        public string DbName
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.ExtDbName);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.ExtDbName, value, AttributeType.String);
        }
        public string DbQueryLanguage
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.ExtDbQueryLanguage);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.ExtDbQueryLanguage, value, AttributeType.String);
        }
        public string DbType
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.ExtDbType);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.ExtDbType, value, AttributeType.String);
        }
        public CtDocument DbMapping
        {
            get => RdfReader.GetDocument(Container, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.ExtDbMapping).AsNode());
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.ExtDbMapping, value.AsUri(), AttributeType.Uri, true);
        }


        internal ExtDatabaseLink(InformationContainer container, string connectionString, string databaseName, string databaseType, string queryLanguage, CtDocument mappingFile = null)
            : base(container, "", databaseName, databaseType, queryLanguage)
        {
            DbConnectionString = connectionString;
            DbName = databaseName;
            DbQueryLanguage = queryLanguage;
            DbType = databaseType;
            if (mappingFile != null)
                DbMapping = mappingFile;

        }

        private ExtDatabaseLink(InformationContainer container, INode resource) : base(container, resource)
        {
        }

        public new static ExtDatabaseLink Read(InformationContainer container, INode resource)
        {
            try
            {
                return new ExtDatabaseLink(
                    container ?? throw new ArgumentNullException(nameof(container)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logger.Log("Argument Exception: " + exception, Logger.MsgType.Warning, "ExtDatabaseLink.Read");
                return null;
            }
        }

        public bool ConvertToPayload(out IcddPayloadTriples convertedTriples)
        {
            convertedTriples = null;
            if (string.IsNullOrEmpty(DbConnectionString) || string.IsNullOrEmpty(DbType)) return false;

            Logger.Log("Started R2RMLConversion",
                Logger.MsgType.Info, "ConvertToPayload.Read");
            IGraph mappingsGraph = new Graph();
            try
            {
                var tripleStore = new TripleStore();
                using IDbConnection connection = new SqlConnection(DbConnectionString);
                var baseUri = Container.IndexGraph.BaseUri.AbsoluteUri.TrimEnd('/') + "/triples-" +
               ContainerHandler.Encode(System.Guid.NewGuid()) + "/";

                MappingOptions mappingOptions = new MappingOptions().WithBaseUri(baseUri);
                IR2RMLProcessor processor = new W3CR2RMLProcessor(connection, mappingOptions);
                IR2RML rml = null;

                if (DbMapping != null)
                {

                    FileLoader.Load(mappingsGraph, DbMapping.AbsoluteFilePath());
                    if (mappingsGraph.IsEmpty)
                        return false;
                    rml = R2RMLLoader.LoadFile(DbMapping.AbsoluteFilePath(), mappingOptions);
                }
                else
                {
                    rml = GenerateDirectMapping(DbConnectionString, baseUri);
                }
                processor.GenerateTriples(rml, tripleStore);
                if (tripleStore.IsEmpty)
                {
                    Logger.Log("No triples have been generated.",
                        Logger.MsgType.Error, "ConvertToPayload.Read");
                    return true;
                }
                CompressingTurtleWriter wr = new(TurtleSyntax.W3C);
                var folderString = Path.Combine(FileHandler.GetWorkFolder(), "Converter", "R2RML");
                Directory.CreateDirectory(folderString);
                var nameString = this.DbMapping != null ? Path.ChangeExtension(DbMapping.Name.Replace(".mappings.", "."), null) : DbName;
                var tempFile = Path.Combine(folderString, nameString + ".instances.ttl");
                try
                {
                    IGraph resultGraph = tripleStore.Graphs.First();
                    UriNode firstNode = resultGraph.Triples.First().Subject as UriNode;
                    if (firstNode != null)
                    {
                        resultGraph.BaseUri = new Uri(firstNode.Uri.AbsoluteUri.Split('#').First());
                    }


                    if (!mappingsGraph.IsEmpty)
                    {
                        resultGraph.NamespaceMap.Import(mappingsGraph.NamespaceMap);
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("Database Exception: " + e, Logger.MsgType.Error, "ExtDatabaseLink.ConvertToPayload");
                }

                wr.Save(tripleStore.Graphs.First(), tempFile);

                convertedTriples = Container.CreatePayloadTriple(tempFile, true);
                return true;
            }
            catch (Exception exception)
            {
                Logger.Log("Database Exception: " + exception, Logger.MsgType.Error, "ExtDatabaseLink.ConvertToPayload");
                return false;
            }
        }

        public List<SqlResult> ExecuteQuery(List<string> queries)
        {
            List<SqlResult> results = new();
            using SqlConnection connection = new(DbConnectionString);
            connection.Open();
            foreach (var sqlString in queries)
            {
                var sqlQueryString1 = Regex.Replace(sqlString, "\\\\n+\\s*GO", "");
                var sqlQueryString = Regex.Unescape(sqlQueryString1).TrimStart('\"').TrimEnd('\"').TrimStart().TrimEnd();
                SqlCommand command = new(sqlQueryString, connection);
                try
                {
                    var result = command.ExecuteNonQuery();
                    results.Add(result > 0
                        ? new SqlResult(sqlQueryString, true, result + " rows affected")
                        : new SqlResult(sqlQueryString, false, result + " rows affected"));
                }
                catch (Exception e)
                {
                    var exception = new IcddException("Error during SQL Query", e);
                    results.Add(new SqlResult(sqlQueryString, false, exception.Message + ": " + e.Message));
                }

            }
            connection.Close();
            return results;
        }

        public SqlResult ExecuteQuery(string query)
        {
            return ExecuteQuery(new List<string>() { query }).FirstOrDefault();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return base.Equals(obj);
            }
            else
            {
                CtExternalDocument p = (CtExternalDocument)obj;
                return base.Equals(obj) && (Url == p.Url);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.ExtDocDatabaseLink;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.ExtDocDatabaseLink);
        }

        internal static DirectR2RMLMapping GenerateDirectMapping(string connection, string baseUri)
        {
            var dbSchema = new DatabaseSchemaAdapter(
                new DatabaseReader(new SqlConnection(connection)),
                new MSSQLServerColumTypeMapper());

            return new DirectR2RMLMapping(dbSchema, new MappingOptions().WithBaseUri(baseUri));

        }
    }
}