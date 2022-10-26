using System;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF.Query.Builder;

namespace IIB.ICDD.Querying
{
    public static class IcddSparqlQueryBuilder
    {
        public static IcddSparqlQuery Query(string queryString)
        {
            IcddSparqlQuery result = new();
            result.Parse(queryString);
            return result;
        }

        public static IcddSparqlQuery Query(string queryString, string resultTemplate)
        {
            IcddSparqlQuery result = new();
            result.Parse(queryString, resultTemplate);
            return result;
        }
    }
}