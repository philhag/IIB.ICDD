using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Builder;

namespace IIB.ICDD.Querying
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddSparqlQuery 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddSparqlQuery
    {
        internal SparqlQueryParser Parser = new();
        internal SparqlParameterizedString QueryString = new();
        public SparqlQuery SparqlQuery;
        internal SparqlResultTemplate Template;

        public IcddSparqlQuery()
        {
            QueryString.Namespaces = IcddNamespacesHelper.ICDD_NAMESPACE_MAP;
        }

        public void Parse(string sparqlQueryString)
        {
            QueryString.CommandText = sparqlQueryString;
            SparqlQuery = Parser.ParseFromString(QueryString);
        }

        public void Parse(string sparqlQueryString, string resultTemplate)
        {
            QueryString.CommandText = sparqlQueryString;
            SparqlQuery = Parser.ParseFromString(QueryString);
            Template = new SparqlResultTemplate(resultTemplate);
        }

        public void Parse(IQueryBuilder queryBuilder)
        {
            queryBuilder.Prefixes.Import(QueryString.Namespaces);
            SparqlQuery = queryBuilder.BuildQuery();
        }

        public void AddNamespace(string prefix, Uri uri)
        {
            QueryString.Namespaces.AddNamespace(prefix, uri);
        }
    }

    public class SparqlResultTemplate
    {
        public string TemplateString { get; set; }
        public List<string> Variables { get; set; }

        public SparqlResultTemplate(string templateString)
        {
            TemplateString = templateString;
            string pattern = @"\?(([a-z]|[A-z]|[1-9])+)";
            var matches = Regex.Matches(templateString, pattern, RegexOptions.IgnoreCase);
            Variables = new List<string>();
            foreach (Match match in matches)
            {
                Variables.Add(match.Value.Replace("?",""));
            }
        }
    }
}
