using IIB.ICDD.Model.Interfaces;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Relations
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsContainsRelation 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsRelation" />
    public class DsContainsRelation : DsRelation
    {
        protected DsContainsRelation PriorContainsRelationVersion;
        // protected DsContainsRelationGroup GroupedBy;
        protected DsAssembly HasAssembly;
        protected DsPart HasPart;
        public DsContainsRelation(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }
    }
}
