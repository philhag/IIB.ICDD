using System.Collections.Generic;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Relations
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsConnection 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsRelation" />
    public class DsConnection : DsRelation
    {
        protected List<DsEntity> HasConnectedEntities;
        protected DsConnection(Graph graph, InformationContainer container, INode resource, DsEntity entity1, DsEntity entity2) : base(graph,container, resource)
        {
        }
        protected DsConnection(Graph graph, InformationContainer container, INode resource) : base(graph,container, resource)
        {
        }
    }
}
