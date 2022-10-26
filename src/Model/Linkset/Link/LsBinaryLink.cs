using System;
using System.Linq;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Linkset.Link
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsBinaryLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsLink" />
    public class LsBinaryLink : LsLink
    {
        public LsLinkElement First
        {
            get => HasLinkElements.FirstOrDefault();
            set
            {
                if (HasLinkElements.Count >= 2)
                {
                    int i = 1;
                    foreach (var elem in HasLinkElements)
                    {

                        if (i != 2)
                        {
                            DeleteLinkElement(elem);
                        }
                        i++;
                    }

                }
                AddLinkElement(value);
            }
        }

        public LsLinkElement Second
        {
            get => HasLinkElements.LastOrDefault();
            set
            {
                if (HasLinkElements.Count >= 2)
                {
                    int i = 1;
                    foreach (var elem in HasLinkElements)
                    {

                        if (i != 1)
                        {
                            DeleteLinkElement(elem);
                        }
                        i++;
                    }

                }
                AddLinkElement(value);
            }
        }


        internal LsBinaryLink(CtLinkset linkset, LsLinkElement element, LsLinkElement otherElement) :
            base(linkset)
        {
            First = element;
            Second = otherElement;
        }

        protected LsBinaryLink(CtLinkset linkset, INode resource) :
            base(linkset, resource)
        {
        }

        public new static LsBinaryLink Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsBinaryLink(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsDirectedBinaryLink.Read");
                return null;
            }
        }

        public override bool RefersToDocument(CtDocument document)
        {
            return Equals(First.HasDocument, document) || Equals(Second.HasDocument, document);
        }
        public override string ToString()
        {
            return "BinaryLink: " + First + " <-> " + Second;
        }

        public override bool IsValid()
        {
            return First != null && Second != null;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsBinaryLink;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsBinaryLink);
        }
    }
}
