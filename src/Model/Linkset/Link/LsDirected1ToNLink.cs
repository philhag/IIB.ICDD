using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Linkset.Link
{

    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsDirected1ToNLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsDirectedLink" />
    public class LsDirected1ToNLink : LsDirectedLink
    {
        public LsLinkElement FromElement
        {
            get => FromElements.FirstOrDefault();
            set => FromElements = new List<LsLinkElement> { value };
        }

        internal LsDirected1ToNLink(CtLinkset linkset, LsLinkElement fromElement, List<LsLinkElement> toElements) :
            base(linkset, new List<LsLinkElement> { fromElement }, toElements)
        {}

        protected LsDirected1ToNLink(CtLinkset linkset, INode resource) :
            base(linkset, resource)
        {
        }

        public new static LsDirected1ToNLink Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsDirected1ToNLink(
                  linkset ?? throw new ArgumentNullException(nameof(linkset)),
                  resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsDirected1ToNLink.Read");
                return null;
            }
        }

        public override string ToString()
        {
            string s = "Directed1toNLink: " + FromElement + "-->";
            foreach (var elements in ToElements)
            {
                s += elements + " - ";
            }
            s.Remove(s.Length - 3);
            return s;
        }

        public override bool IsValid()
        {
            return FromElement != null && ToElements is { Count: > 0 };
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsDirected1toNLink;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsDirected1toNLink);
        }
    }
}
