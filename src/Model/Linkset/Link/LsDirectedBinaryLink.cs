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
    /// Class:  LsDirectedBinaryLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsDirectedLink" />
    public class LsDirectedBinaryLink : LsDirectedLink
    {
        public LsLinkElement FromElement
        {
            get => FromElements.FirstOrDefault();
            set => FromElements = new List<LsLinkElement> { value };
        }

        public LsLinkElement ToElement
        {
            get => ToElements.FirstOrDefault();
            set => ToElements = new List<LsLinkElement> { value };
        }

        internal LsDirectedBinaryLink(CtLinkset linkset, LsLinkElement fromElement, LsLinkElement toElement) :
            base(linkset, new List<LsLinkElement> { fromElement }, new List<LsLinkElement> { toElement })
        {
            FromElement = fromElement;
            ToElement = toElement;

        }
        
        internal LsDirectedBinaryLink(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public new static LsDirectedBinaryLink Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsDirectedBinaryLink(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsDirectedBinaryLink.Read");
                return null;
            }
        }


        public override string ToString()
        {
            string s = "DirectedBinaryLink: ";
            s += FromElement + " --> " + ToElement;
            return s;
        }

        public override bool IsValid()
        {
            return FromElement != null && ToElement != null;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsDirectedBinaryLink;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsDirectedBinaryLink);
        }
    }
}
