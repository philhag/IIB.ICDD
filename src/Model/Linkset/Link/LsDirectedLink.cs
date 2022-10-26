using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Linkset.Link
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsDirectedLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsLink" />
    public class LsDirectedLink : LsLink
    {
        public new List<LsLinkElement> HasLinkElements
        {
            get
            {
                var results = new List<LsLinkElement>();
                results.AddRange(FromElements);
                results.AddRange(ToElements);
                return results;
            }
        }
        public List<LsLinkElement> FromElements
        {
            get => RdfReader.GetLinkElementsByLink(Linkset, this, IcddPredicateSpecsHelper.HAS_FROM_LINK_ELEMENT);
            set
            {
                RdfWriter.ClearDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_FROM_LINK_ELEMENT);
                foreach (var linkElement in value)
                {
                    RdfWriter.AddDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_FROM_LINK_ELEMENT, linkElement.AsUri(), AttributeType.Uri); ;
                }
            }
        }

        public List<LsLinkElement> ToElements
        {
            get => RdfReader.GetLinkElementsByLink(Linkset, this, IcddPredicateSpecsHelper.HAS_TO_LINK_ELEMENT);
            set
            {
                RdfWriter.ClearDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_TO_LINK_ELEMENT);
                foreach (var linkElement in value)
                {
                    RdfWriter.AddDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_TO_LINK_ELEMENT, linkElement.AsUri(), AttributeType.Uri); ;
                }
            }
        }

        internal LsDirectedLink(CtLinkset linkset, List<LsLinkElement> fromElements, List<LsLinkElement> toElements) :
            base(linkset)
        {

            FromElements = fromElements;
            ToElements = toElements;
        }

        protected LsDirectedLink(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public new static LsDirectedLink Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsDirectedLink(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsDirectedLink.Read");
                return null;
            }
        }

        public override bool RefersToDocument(CtDocument document)
        {
            return HasLinkElements.Any(element => Equals(element.HasDocument, document));
        }

        public override string ToString()
        {
            string s = "DirectedLink: ";
            foreach (var elements in FromElements)
            {
                s += elements.ToString() + " - ";
            }
            s.Remove(s.Length - 3);
            s += " --> ";
            foreach (var elements in ToElements)
            {
                s += elements.ToString() + " - ";
            }
            s.Remove(s.Length - 3);
            return s;
        }

        public override bool IsValid()
        {
            return FromElements != null && ToElements != null && FromElements.Count>0 && ToElements.Count>0;
        }
        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsDirectedLink;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsDirectedLink);
        }
    }
}
