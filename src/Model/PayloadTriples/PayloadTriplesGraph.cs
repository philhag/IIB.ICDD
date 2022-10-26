using System;
using System.Collections.Generic;
using System.Linq;
using VDS.Common.Collections.Enumerations;
using VDS.RDF;
using VDS.RDF.Ontology;

namespace IIB.ICDD.Model.PayloadTriples
{
    public class PayloadTriplesGraph : OntologyGraph
    {
        public PayloadTriplesGraph() : base()
        {
        }

        public List<Individual> GetIndividuals()
        {
            var results = new List<Individual>();
            var triples = GetTriplesWithPredicate(CreateUriNode(new Uri(OntologyHelper.PropertyType))).ToList().Select(m => m.Subject).Distinct();

            foreach (INode subject in triples)
            {
                try
                {
                    results.Add(CreateIndividual(subject));
                }
                catch
                {
                    continue;
                }
            }

            return results.Distinct().ToList();
        }

        public List<OntologyResource> GetR2RmlMappings()
        {
            var results = new List<OntologyResource>();
            var triples = GetTriplesWithPredicate(CreateUriNode(new Uri("http://www.w3.org/ns/r2rml#logicalTable"))).ToList().Select(m => m.Subject).Distinct().ToList();
            triples.AddRange(GetTriplesWithPredicate(CreateUriNode(new Uri("http://www.w3.org/ns/r2rml#LogicalTable"))).ToList().Select(m => m.Subject).Distinct());

            foreach (INode subject in triples)
            {
                try
                {
                    results.Add(CreateOntologyResource(subject));
                }
                catch
                {
                    continue;
                }
            }

            return results.Distinct().ToList();
        }
    }
}
