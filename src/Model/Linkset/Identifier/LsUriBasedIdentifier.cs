using System;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Linkset.Identifier
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsUriBasedIdentifier 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsIdentifier" />
    public class LsUriBasedIdentifier : LsIdentifier
    {

        public Uri Uri
        {
            get => RdfReader.GetDataPropertyAsUri(Graph, this, IcddPredicateSpecsHelper.URI);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.URI, value, AttributeType.Uri);

        }

        internal LsUriBasedIdentifier(CtLinkset linkset, Uri uri) :
            base(linkset)
        {
            Uri = uri;
        }

        private LsUriBasedIdentifier(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public static LsUriBasedIdentifier Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsUriBasedIdentifier(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsUriBasedIdentifier.Read");
                return null;
            }
        }

        public override string ToString()
        {
            return "[" + Uri + "]";
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsURIBasedIdentifier;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsURIBasedIdentifier);
        }
    }
}
