using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IIB.ICDD.Conversion.Container;
using IIB.ICDD.Handling;
using IIB.ICDD.Model;
using IIB.ICDD.Reasoning;
using VDS.RDF;
using VDS.RDF.Ontology;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using VDS.RDF.Writing.Formatting;

namespace IIB.ICDD.Querying
{

    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddSparqlProcessor 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddSparqlProcessor
    {
        internal TripleStore Store;
        internal IcddSparqlQuery Query;
        internal LeviathanQueryProcessor Processor;

        public IcddSparqlProcessor(InformationContainer container, IcddSparqlQuery query, bool applyInference = false)
        {
            Store = new TripleStore();
            TriGWriter trigwriter = new();
            if (applyInference)
            {
                Store.Add(IcddRdfsReasoner.Create(container).ApplyInference());
                trigwriter.Save(Store, Path.Combine(Directory.GetParent(container.PathToContainer)?.FullName, Path.ChangeExtension(container.ContainerName, null) + "_tripleStore.ttl"));

                Store.ExecuteUpdate(@"
                    INSERT { 
                        ?ident1 <https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#linkedIdentifier> ?ident2 . 
                        ?ident1 <https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#linkedElement> ?elem2 . 
                        ?ident1 <https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#linkedDocument> ?document2 . 

                        ?ident2 <https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#linkedIdentifier> ?ident1 . 
                        ?ident2 <https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#linkedElement> ?elem1 . 
                        ?ident2 <https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#linkedDocument> ?document1 . 
                    }
                    WHERE {
                        ?link a <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#Link> .
                        ?link <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasFromLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasToLinkElement> ?elem1 .
                        ?link <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasToLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasFromLinkElement> ?elem2 .
                      
                    OPTIONAL{
                        ?elem1 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasIdentifier> ?ident1 .
                        ?elem2 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasIdentifier> ?ident2 . 
                        ?elem1 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasDocument> ?document1 .
                        ?elem2 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasDocument> ?document2 . 
                        ?ident1 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#identifier>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#uri> ?entity1 .
                        ?ident2 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#identifier>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#uri> ?entity2 .
                        }

                    FILTER(!sameTerm(?elem1, ?elem2))
                    }
                    
                    ");

                //Store.ExecuteUpdate(@"
                //    INSERT { 
                //        ?rdfEntity1 <http://icdd.vm.rub.de/ontology/icddl#linked> ?rdfEntity2 . 
                        
                //    }
                //    WHERE {
                //        ?link a <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#Link> .
                //        ?link <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasFromLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasToLinkElement> ?elem1 .
                //        ?link <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasToLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasFromLinkElement> ?elem2 .
                      
                //    OPTIONAL{
                //        ?elem1 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasIdentifier> ?ident1 .
                //        ?elem2 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasIdentifier> ?ident2 . 
                //        ?ident1 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#identifier>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#uri> ?entity1 .
                //        ?ident2 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#identifier>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#uri> ?entity2 .
                //        ?rdfEntity1 ?hasProp1 ?entity1 .
                //        ?rdfEntity2 ?hasProp2 ?entity2 .
                //        }

                //    FILTER(!sameTerm(?elem1, ?elem2) && !sameTerm(?ident1, ?rdfEntity1) && !sameTerm(?ident2, ?rdfEntity2))
                //    }
                    
                //    ");
                //Store.ExecuteUpdate(@"
                //    INSERT { 
                //        ?rdfEntity1 <http://icdd.vm.rub.de/ontology/icddl#linkedBinary> ?rdfEntity2 . 
                        
                //    }
                //    WHERE {
                //        ?link a <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#BinaryLink> .
                //        ?link <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasFromLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasToLinkElement> ?elem1 .
                //        ?link <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasToLinkElement>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasFromLinkElement> ?elem2 .
                      
                //    OPTIONAL{
                //        ?elem1 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasIdentifier> ?ident1 .
                //        ?elem2 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#hasIdentifier> ?ident2 . 
                //        ?ident1 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#identifier>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#uri> ?entity1 .
                //        ?ident2 <https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#identifier>|<https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#uri> ?entity2 .
                //        ?rdfEntity1 ?hasProp1 ?entity1 .
                //        ?rdfEntity2 ?hasProp2 ?entity2 .
                //        }

                //    FILTER(!sameTerm(?elem1, ?elem2) && !sameTerm(?ident1, ?rdfEntity1) && !sameTerm(?ident2, ?rdfEntity2))
                //    }
                    
                    //");
                Store.Graphs.First().NamespaceMap.AddNamespace("lse", new Uri("https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#"));
                //Store.Graphs.First().NamespaceMap.AddNamespace("icddl", new Uri("http://icdd.vm.rub.de/ontology/icddl#"));

            }
            else
            {
                Store.Add((new IcddConverter(container).ConvertToGraph()));
            }

            Query = query;
            Processor = new LeviathanQueryProcessor(Store);
            foreach (var payloadTriple in container.PayloadTriples)
            {
                Query.SparqlQuery.NamespaceMap.Import(payloadTriple.Namespaces);
            }
            foreach (var ontologies in container.UserDefinedOntologies)
            {
                Query.SparqlQuery.NamespaceMap.Import(ontologies.Namespaces);
            }
            Query.SparqlQuery.NamespaceMap.AddNamespace("lse", new Uri("https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#"));
            trigwriter.Save(Store, Path.Combine(Directory.GetParent(container.PathToContainer)?.FullName, Path.ChangeExtension(container.ContainerName, null) + "_aggregatedStore.ttl"));
        }

        public string ExecuteQuery()
        {
            if (Query == null || Processor == null)
                throw new IcddException("Query or processor not initialized.", new ArgumentNullException());

            if (Query.SparqlQuery.QueryType == SparqlQueryType.Construct)
                throw new IcddException("Query is malformed. CONSTRUCT queries are not supported.", new InvalidOperationException());

            if (Query.SparqlQuery.QueryType == SparqlQueryType.Ask)
                throw new IcddException("Query is malformed. ASK queries are not supported.", new InvalidOperationException());

            if (Query.SparqlQuery.QueryType == SparqlQueryType.Describe)
                throw new IcddException("Query is malformed. DESCRIBE queries are not supported.", new InvalidOperationException());

            var results = Processor.ProcessQuery(Query.SparqlQuery);
            if (!(results is SparqlResultSet resultSet))
                throw new IcddException("Query is malformed. CONSTRUCT queries are not supported.", new InvalidOperationException());

            SparqlJsonWriter writer = new();
            StringBuilder build = new();
            System.IO.StringWriter sw = new(build);
            writer.Save(resultSet, sw);
            return build.ToString();
        }

        public List<string> ExecuteQueryWithTemplate()
        {
            if (Query == null || Processor == null)
                throw new IcddException("Query or processor not initialized.", new ArgumentNullException());

            if (Query.Template == null)
                throw new IcddException("Template not initialized.", new ArgumentNullException());

            var results = Processor.ProcessQuery(Query.SparqlQuery);
            if (!(results is SparqlResultSet resultSet))
                throw new IcddException("Query is malformed.", new ArgumentNullException());

            INodeFormatter nodeFormatter = ContainerHandler.GetPlainNodeFormatter();
            var filledTemplates = new List<string>();
            foreach (SparqlResult result in resultSet)
            {
                var resultTemplateString = Query.Template.Variables.Aggregate(Query.Template.TemplateString, (current, variable) => current.Replace("\"?" + variable + "\"", result.TryGetBoundValue(variable, out INode value) ? nodeFormatter.Format(value) : "NULL"));
                filledTemplates.Add(resultTemplateString);
            }
            return filledTemplates;

        }
    }
}
