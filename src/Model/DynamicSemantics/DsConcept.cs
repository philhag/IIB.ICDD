using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsConcept 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsInformationModel" />
    public class DsConcept : DsInformationModel
    {
        protected DsConcept PriorVersion;
        // HasProperties
        protected string Description;
        protected string Name;

        protected DsConcept(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }

        protected DsConcept(Graph graph, InformationContainer container) : base(graph, container)
        {
        }


        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CONCEPT;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CONCEPT);
        }
    }
}
