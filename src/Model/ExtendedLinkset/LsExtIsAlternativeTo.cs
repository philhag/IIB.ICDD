using System;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Model.Linkset.Link;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.ExtendedLinkset
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsExtIsAlternativeTo 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsBinaryLink" />
    public class LsExtIsAlternativeTo : LsDirectedBinaryLink
    {
       
        internal LsExtIsAlternativeTo(CtLinkset linkset, LsLinkElement element, LsLinkElement otherElement) :
            base(linkset, element, otherElement)
        {
        }

        protected LsExtIsAlternativeTo(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public new static LsExtIsAlternativeTo Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsExtIsAlternativeTo(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtOrganisation.Read");
                return null;
            }
        }

        public override string ToString()
        {
            return "LsExtIsAlternativeTo: " + FromElement + " <-> " + ToElement;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.ExLsIsAlternativeTo;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsAlternativeTo);
        }
    }
}
