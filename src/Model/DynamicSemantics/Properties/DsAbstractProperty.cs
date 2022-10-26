using IIB.ICDD.Model.Interfaces;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Properties
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsAbstractProperty 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="IcddBaseElement" />
    public abstract class DsAbstractProperty : IcddBaseElement
    {
        protected DsConcept BelongsToConcept;
        protected DsAbstractProperty PriorVersion;
        protected DsAbstractProperty(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }
        protected DsAbstractProperty(Graph graph, InformationContainer container) : base(graph, container)
        {
        }
    }
}
