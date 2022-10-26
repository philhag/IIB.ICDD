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
    /// Class:  LsExtIsMemberOf 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsDirected1ToNLink" />
    public class LsExtIsMemberOf : LsDirected1ToNLink
    {
        internal LsExtIsMemberOf(CtLinkset linkset, LsLinkElement fromElement, List<LsLinkElement> toElements) :
            base(linkset, fromElement, toElements)
        {
        }

        protected LsExtIsMemberOf(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public new static LsExtIsMemberOf Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsExtIsMemberOf(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsExtIsMemberOf.Read");
                return null;
            }
        }

        public override string ToString()
        {
            string s = "LsExtIsMemberOf: " + FromElement + "-->";
            foreach (var elements in ToElements)
            {
                s += elements + " - ";
            }
            s.Remove(s.Length - 3);
            return s;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.ExLsIsMemberOf;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsMemberOf);
        }
    }
}
