using System;
using System.Collections.Generic;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Relations
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsContainsRelationGroup 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="IIB.ICDD.Model.IcddBaseElement" />
    public class DsContainsRelationGroup : IcddBaseElement
    {
        protected DsAssembly BelongsToAssembly;
        protected DsContainsRelationGroup PriorContainsRelationGroupVersion;
        protected List<DsContainsRelation> Groups;
        protected DsContainsRelationGroup(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CONTAINS_RELATION_GROUP;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.RELATION);
        }
    }
}