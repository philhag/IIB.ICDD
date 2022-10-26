using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Relations
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsRelation 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="ICDDToolkitLibrary.ContainerModel.IcddObject" />
    public class DsRelation : IcddBaseElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DsRelation"/> class.
        /// </summary>
        /// <param name="graph">The graph to which this DsRelation belongs to.</param>
        /// <param name="container">The ICDD container this Object belongs to.</param>
        /// <param name="resource">The RDF resource of this object.</param>
        protected DsRelation(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.RELATION;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.RELATION);
        }
    }
}
