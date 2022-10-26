using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsInformationModel 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="IcddBaseElement" />
    public abstract class DsInformationModel : IcddBaseElement
    {
        protected DsInformationModel(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }

        protected DsInformationModel(Graph graph, InformationContainer container) : base(graph, container)
        {
        }
    }
}
