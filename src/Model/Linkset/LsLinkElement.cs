using System;
using IIB.ICDD.Handling;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.DynamicSemantics.Properties;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset.Identifier;
using IIB.ICDD.Model.Linkset.Link;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Linkset
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsLinkElement 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsComplexProperty" />
    public class LsLinkElement : DsComplexProperty
    {
        internal CtLinkset Linkset;
        public LsIdentifier HasIdentifier
        {
            get => RdfReader.GetIdentifier(Linkset, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.HAS_IDENTIFIER)?.AsNode());
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_IDENTIFIER, value.AsUri(), AttributeType.Uri);
        }

        public CtDocument HasDocument
        {
            get => RdfReader.GetDocument(Container, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.HAS_DOCUMENT).AsNode());
            set
            {
                if (value != null)
                    RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_DOCUMENT, value.AsUri(), AttributeType.Uri);
            }
        }

        internal LsLinkElement(CtLinkset linkset, CtDocument document) :
            base(linkset.LinksetGraph, linkset.Container)
        {
            Linkset = linkset;
            HasDocument = document;
        }
        internal LsLinkElement(CtLinkset linkset, CtDocument document, LsIdentifier identifier) :
            base(linkset.LinksetGraph, linkset.Container)
        {
            Linkset = linkset;
            HasDocument = document;
            HasIdentifier = identifier;
        }

        private LsLinkElement(CtLinkset linkset, INode resource) : base(linkset.LinksetGraph, linkset.Container, resource)
        {
            Linkset = linkset;
        }

        public static LsLinkElement Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsLinkElement(
                   linkset ?? throw new ArgumentNullException(nameof(linkset)),
                   resource ?? throw new ArgumentNullException(nameof(resource)));

            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsLinkElement.Read");
                return null;
            }
        }

        public override string ToString()
        {
            return HasDocument?.Name + (HasIdentifier != null ? " (Identifier: " + HasIdentifier + ")" : "");
        }

        public override IGraph AsSubgraph()
        {
            Graph g = new();
            g.BaseUri = Linkset.LinksetGraph.BaseUri;
            g.Merge(base.AsSubgraph());
            g.Merge(HasDocument.AsSubgraph());
            if (HasIdentifier != null)
                g.Merge(HasIdentifier.AsSubgraph());
            return g;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsLinkElement;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsLinkElement);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return base.Equals(obj);
            }
            else
            {
                LsLinkElement p = (LsLinkElement)obj;
                var equalIdentifier = true;
                if (HasIdentifier != null && p.HasIdentifier != null)
                    equalIdentifier = HasIdentifier.Equals(p.HasIdentifier);

                var equalIDocument = true;
                if (HasDocument != null && p.HasDocument != null)
                    equalIDocument = HasDocument.Equals(p.HasDocument);

                return base.Equals(obj)
                    && equalIDocument
                    && equalIdentifier;
            }
        }

        protected bool Equals(LsLinkElement other)
        {
            return base.Equals(other) && Equals(Linkset, other.Linkset);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Linkset != null ? Linkset.GetHashCode() : 0);
            }
        }
    }
}