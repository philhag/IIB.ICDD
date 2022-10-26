using System.Collections.Generic;
using System.IO;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Interfaces;
using VDS.RDF;
using VDS.RDF.Ontology;
using VDS.RDF.Parsing;

namespace IIB.ICDD.Parsing
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddOntologyParser 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal class IcddOntologyParser
    {
        protected internal string OntologyFile;
        protected internal OntologyGraph OntologyGraph;

        internal IcddOntologyParser(OntologyGraph graph, string ontologyFile)
        {
            OntologyGraph = graph;
            OntologyFile = ontologyFile;
        }

        internal void Parse()
        {
            if (OntologyGraph == null)
            {
                OntologyGraph = new OntologyGraph();
            }
            if (File.Exists(OntologyFile))
            {
                FileLoader.Load(OntologyGraph, OntologyFile);
            }
            else
            {
                if (File.Exists(OntologyFile + ".rdf"))
                {
                    OntologyFile = OntologyFile + ".rdf";
                    FileLoader.Load(OntologyGraph, OntologyFile);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
        }

        internal Dictionary<IcddResourceWrapper, OntologyClass> Classes()
        {

            var result = new Dictionary<IcddResourceWrapper, OntologyClass>();

            if (OntologyGraph != null)
            {
                foreach (var ontologyClass in OntologyGraph.AllClasses)
                {
                    var node = ontologyClass.Resource;
                    if (node.NodeType == NodeType.Uri)
                    {
                        UriNode uri = (UriNode)node;
                        result.TryAdd(new IcddResourceWrapper(uri), ontologyClass);
                    }
                }
            }

            return result;
        }

        //internal Dictionary<string, IcddResourceWrapper> Imports()
        //{

        //    var result = new Dictionary<string, IcddResourceWrapper>();

        //    if (OntologyGraph != null)
        //    {
        //        foreach (var ontologyClass in OntologyGraph.NamespaceMap.)
        //        {
        //            var node = ontologyClass.Resource;
        //            if (node.NodeType == NodeType.Uri)
        //            {
        //                UriNode uri = (UriNode)node;
        //                result.Add(IcddNodeHandler.SeparateFragment(node), new IcddResourceWrapper(uri.Uri));
        //            }
        //        }
        //    }

        //    return result;
        //}

        internal Dictionary<IcddResourceWrapper, OntologyProperty> Properties()
        {
            var result = new Dictionary<IcddResourceWrapper, OntologyProperty>();
            if (OntologyGraph != null)
            {
                foreach (var ontologyProperty in OntologyGraph.AllProperties)
                {
                    result.TryAdd(new IcddResourceWrapper(((UriNode)ontologyProperty.Resource)), ontologyProperty);
                }
            }
            return result;
        }
    }
}
