using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Properties
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsSimpleProperty 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsAbstractProperty" />
    public abstract class DsSimpleProperty : DsAbstractProperty
    {
        protected DsSimpleProperty(Graph graph, InformationContainer container, INode resource) : base(graph,container, resource)
        {
        }

        public abstract object GetPropertyValue();
    }
}
