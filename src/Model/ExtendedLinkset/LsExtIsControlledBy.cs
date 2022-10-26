using System;
using System.Collections.Generic;
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
    /// Class:  LsExtIsControlledBy 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsDirected1ToNLink" />
    public class LsExtIsControlledBy : LsDirected1ToNLink
    {
        internal LsExtIsControlledBy(CtLinkset linkset, LsLinkElement fromElement, List<LsLinkElement> toElements) :
            base(linkset, fromElement, toElements)
        {
        }
        protected LsExtIsControlledBy(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public new static LsExtIsControlledBy Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsExtIsControlledBy(
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
            string s = "LsExtIsControlledBy: " + FromElement + "-->";
            foreach (var elements in ToElements)
            {
                s += elements + " - ";
            }
            s.Remove(s.Length - 3);
            return s;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.ExLsIsControlledBy;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsControlledBy);
        }
    }
}
