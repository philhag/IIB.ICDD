using System.Collections.Generic;
using IIB.ICDD.Model.DynamicSemantics.Relations;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsEntity 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsConcept" />
    public class DsEntity : DsConcept
    {
        protected new DsEntity PriorVersion;
        protected List<DsConnection> HasConnections;
        protected List<DsDirectedConnection> HasIncomingConnections;
        protected List<DsDirectedConnection> HasOutgoingConnections;

        protected DsEntity(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }

        protected DsEntity(Graph graph, InformationContainer container) : base(graph, container)
        {
        }
    }
}
