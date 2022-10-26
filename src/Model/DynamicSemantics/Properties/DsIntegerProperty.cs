using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Properties
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsIntegerProperty 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsNumericProperty" />
    public class DsIntegerProperty : DsNumericProperty
    {
        protected int DatatypeValue;
        protected DsIntegerProperty(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }

        public override object GetPropertyValue()
        {
            return DatatypeValue;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.INTEGER_PROPERTY;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.INTEGER_PROPERTY);
        }

    }
}
