using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Container.ExtendedDocument;
using IIB.ICDD.Parsing.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using Newtonsoft.Json.Linq;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Ontology;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;
using StringWriter = System.IO.StringWriter;

namespace IIB.ICDD.Model.PayloadTriples
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddPayloadTriples 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// 
    [DebuggerDisplay("{BaseUri}", Name = "{BaseUri}")]
    public class IcddPayloadTriples : IRdfFile
    {
        /// <summary>
        /// The file name of the user defined ontology.
        /// </summary>
        protected string FileName;
        protected string FilePath;
        /// <summary>
        /// The ontology graph of the user defined ontology.
        /// </summary>
        protected InformationContainer Container;
        protected PayloadTriplesGraph OntologyGraph;
        public List<Individual> Individuals = new();
        public List<IcddOntology> ReferenceOntologies = new();
        public Uri BaseUri;
        public ExtPayloadProxy Proxy;

        public INamespaceMapper Namespaces => OntologyGraph.NamespaceMap;

        public IcddPayloadTriples(InformationContainer container, string filepath)
        {
            Container = container;
            FileName = Path.GetFileName(filepath);
            FilePath = filepath;
            OntologyGraph = new PayloadTriplesGraph();

            if (FileHandler.IsRdf(filepath))
            {
                try
                {
                    FileLoader.Load(OntologyGraph, filepath);
                }
                catch (Exception e)
                {
                    throw new IcddException("RDF file not well-formed.", e);
                }

            }
            else
            {
                throw new FileNotFoundException();
            }


            try
            {
                BaseUri = OntologyGraph.BaseUri;
                Individuals = OntologyGraph.GetIndividuals();
                Proxy = container.Documents.Find(m => m is ExtPayloadProxy payloadProxy && payloadProxy.BaseUri == BaseUri && payloadProxy.Name == FileName) as ExtPayloadProxy;

                if (Proxy == null)
                {
                    Proxy = new ExtPayloadProxy(container, BaseUri, FileName)
                    {
                        Description = "This ExtPayloadProxy has been added for the payload triples file " + GetName(),
                        VersionId = "1",
                        VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
                    };
                    container.AddDocument(Proxy);
                    container.SaveRdf();
                }

                foreach (var ns in OntologyGraph.NamespaceMap.Prefixes)
                {
                    var uri = OntologyGraph.NamespaceMap.GetNamespaceUri(ns);
                    if (uri != null)
                    {
                        var ontologyRef = container.UserDefinedOntologies.Find(onto => onto.BaseUri == uri);
                        if (ontologyRef == null) continue;
                        ReferenceOntologies.Add(ontologyRef);
                    }
                }

                
            }
            catch (Exception e)
            {
                throw new IcddException("Cannot load triple data.", e);
            }
        }

        public static IcddPayloadTriples Read(InformationContainer container, string filename)
        {
            try
            {
                var file = Path.Combine(container.GetLinksetFolder(), filename);
                return File.Exists(file) ? new IcddPayloadTriples(container, file) : null;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, Logger.MsgType.Error, "IcddPayloadTriples.Read");
                return null;
            }
        }

        public static List<IcddPayloadTriples> ReadAll(InformationContainer container)
        {
            List<IcddPayloadTriples> icddPayloadTriples = new List<IcddPayloadTriples>();
            var files = Directory.GetFiles(container.GetLinksetFolder()).Select(data => new FileInfo(data));
            foreach(var file in files)
            {
                if(!container.Linksets.Any(m => FileHandler.PathEquals(m.Location(), file.FullName))) //kein Linkset ausgewählt
                {
                    var payload = Read(container, file.FullName);
                    if(payload!=null)
                        icddPayloadTriples.Add(payload);
                }

            }
            return icddPayloadTriples;
        }

        public string GetFileName()
        {
            return FileName;
        }

        public Graph GetGraph()
        {
            return OntologyGraph as Graph;
        }

        public string GetFilePath()
        {
            return FilePath;
        }

        public string GetName()
        {
            return Path.GetFileNameWithoutExtension(FileName);
        }

        public bool Delete()
        {
            File.Delete(Path.Combine(Container.GetLinksetFolder(), FileName));
            Proxy.Delete();
            return Container.PayloadTriples.Remove(this);
        }

        public override string ToString()
        {
            return BaseUri.AbsoluteUri;
        }

        public JObject ToJsonLd()
        {
            JsonLdWriter writer = new();
            StringBuilder build = new();
            StringWriter write = new(build);

            TripleStore store = new();
            store.Add(OntologyGraph);
            writer.Save(store, write);

            //no context given
            return JsonLdProcessor.Compact(writer.SerializeStore(store), JToken.Parse(IcddContextHelper.FullContext), new JsonLdProcessorOptions());
        }

        public string ToTurtleString()
        {
            var ext = Path.GetExtension(FileName)?.ToLower();
            IRdfWriter writer = new CompressingTurtleWriter(TurtleSyntax.W3C);
            StringBuilder build = new();
            StringWriter write = new(build);
            writer.Save(OntologyGraph, write);
            return write.ToString();

        }


        public bool SaveRdf()
        {
            var ext = Path.GetExtension(FileName)?.ToLower();
            IRdfWriter writer = ext switch
            {
                "ttl" => new CompressingTurtleWriter(TurtleSyntax.W3C),
                "rdf" => new PrettyRdfXmlWriter(),
                _ => new PrettyRdfXmlWriter(),
            };
            if (string.IsNullOrEmpty(FileName)) return false;
            writer.Save(OntologyGraph, FileName);
            return true;
        }
    }
}
