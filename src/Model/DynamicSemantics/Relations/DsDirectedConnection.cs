using System.Collections.Generic;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Relations
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsDirectedConnection 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsConnection" />
    public class DsDirectedConnection : DsConnection
    {
        protected DsEntity FromEntity;
        protected DsEntity ToEntity;
        protected DsDirectedConnection(Graph graph, InformationContainer container, INode resource, DsEntity fromEntity, DsEntity toEntity) : base(graph, container, resource)
        {
            HasConnectedEntities = new List<DsEntity> {fromEntity, toEntity};
            FromEntity = fromEntity;
            ToEntity = toEntity;

        }
    }
}