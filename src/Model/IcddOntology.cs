using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using VDS.RDF;
using VDS.RDF.Ontology;

namespace IIB.ICDD.Model
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddUserDefinedOntology 
    /// (c) 2020 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// 
    [DebuggerDisplay("{BaseUri}", Name = "{BaseUri}")]
    public class IcddOntology
    {
        /// <summary>
        /// The file name of the user defined ontology.
        /// </summary>
        protected string FileName;
        /// <summary>
        /// The ontology graph of the user defined ontology.
        /// </summary>
        protected OntologyGraph OntologyGraph;
        protected Ontology Ontology;
        protected Dictionary<IcddResourceWrapper, OntologyClass> OntologyClasses = new();
        protected Dictionary<IcddResourceWrapper, OntologyProperty> OntologyProperties = new();

        public Uri BaseUri;
        public INamespaceMapper Namespaces => OntologyGraph.NamespaceMap;

        public IcddOntology(string filepath)
        {
            FileName = Path.GetFileName(filepath);
            if (File.Exists(filepath))
            {
                OntologyGraph = new OntologyGraph();

                IcddOntologyParser parser = new(OntologyGraph, filepath);

                try
                {
                    parser.Parse();
                    OntologyClasses = parser.Classes();
                    OntologyProperties = parser.Properties();

                    var ontoNode = OntologyGraph.GetTriplesWithPredicateObject(
                        OntologyGraph.CreateUriNode(new Uri(OntologyHelper.PropertyType)),
                        OntologyGraph.CreateUriNode(new Uri(OntologyHelper.OwlOntology)))
                        .First();
                    Ontology = new Ontology(ontoNode.Subject, OntologyGraph);
                    BaseUri = ((UriNode)Ontology.Resource).Uri;

                }
                catch (Exception e)
                {
                    Logger.Log("Cannot read Ontology." + e.Message,Logger.MsgType.Error, "IcddOntology.Constructor");
                }
            }

        }

        public string GetFileName()
        {
            return FileName;
        }

        public Dictionary<IcddResourceWrapper, OntologyClass> Classes()
        {
            return OntologyClasses;
        }

        public static Dictionary<IcddResourceWrapper, Individual> Instances(IcddOntology g, OntologyClass c)
        {
            var nodes = new Dictionary<IcddResourceWrapper, Individual>();
            var triples = g.OntologyGraph.GetTriplesWithPredicateObject(g.OntologyGraph.CreateUriNode(new Uri(OntologyHelper.PropertyType)), c.Resource);
            foreach (Triple t in triples)
            {
                nodes.Add(new IcddResourceWrapper(t.Subject as IUriNode), new Individual(t.Subject, g.OntologyGraph));
            }
            return nodes;
        }

        public Dictionary<IcddResourceWrapper, OntologyProperty> Properties()
        {
            return OntologyProperties;
        }

        public Ontology GetOntology()
        {
            return Ontology;
        }

        public override string ToString()
        {
            return BaseUri.AbsoluteUri;
        }
    }
}
