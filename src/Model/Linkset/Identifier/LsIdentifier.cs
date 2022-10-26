using System;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.DynamicSemantics.Properties;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Linkset.Identifier
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsIdentifier 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsComplexPropertyValue" />
    public abstract class LsIdentifier : DsComplexPropertyValue
    {
        internal CtLinkset Linkset;
        protected LsIdentifier(CtLinkset linkset, INode resource) : base(linkset.LinksetGraph, linkset.Container, resource)
        {
            Linkset = linkset;
        }

        protected LsIdentifier(CtLinkset linkset) : base(linkset.LinksetGraph, linkset.Container)
        {
            Linkset = linkset;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsIdentifier;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsIdentifier);
        }
    }
}