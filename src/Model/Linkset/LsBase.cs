using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Linkset
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsBase 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class LsBase : IcddBaseElement
    {
        public LsBase(Graph graph, InformationContainer container) : base(graph, container)
        {
            RdfWriter.SetImports(graph, IcddNamespacesHelper.LINKSET_ONTOLOGY);
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.OwlOntology;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.OwlOntology);
        }
    }
}
