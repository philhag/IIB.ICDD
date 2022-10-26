using System;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Properties
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsComplexPropertyValue 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsAbstractProperty" />
    public class DsComplexPropertyValue : DsAbstractProperty
    {
        protected DsComplexPropertyValue(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }
        protected DsComplexPropertyValue(Graph graph, InformationContainer container) : base(graph, container)
        {
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.COMPLEX_PROPERTY_VALUE;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.COMPLEX_PROPERTY_VALUE);
        }
    }
}