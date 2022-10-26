using System;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Properties
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsComplexProperty 
    /// (c) 2020 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsAbstractProperty" />
    public class DsComplexProperty : DsAbstractProperty
    {
        protected DsComplexProperty(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }
        protected DsComplexProperty(Graph graph, InformationContainer container) : base(graph, container)
        {
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.COMPLEX_PROPERTY;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.COMPLEX_PROPERTY);
        }

    }
}