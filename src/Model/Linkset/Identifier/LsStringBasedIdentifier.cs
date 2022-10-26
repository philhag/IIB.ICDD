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
    /// Class:  LsStringBasedIdentifier 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsIdentifier" />
    public class LsStringBasedIdentifier : LsIdentifier
    {


        public string Identifier
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.IDENTIFIER);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.IDENTIFIER, value, AttributeType.String);

        }

        public string IdentifierField
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.IDENTIFIER_FIELD);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.IDENTIFIER_FIELD, value, AttributeType.String);

        }


        internal LsStringBasedIdentifier(CtLinkset linkset, string identifierField, string identifier) :
            base(linkset)
        {
            IdentifierField = identifierField;
            Identifier = identifier;
        }

        private LsStringBasedIdentifier(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public static LsStringBasedIdentifier Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsStringBasedIdentifier(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsStringBasedIdentifier.Read");
                return null;
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(IdentifierField))
            {
                return "[" + Identifier + "]";
            }
            if(string.IsNullOrEmpty(IdentifierField) && string.IsNullOrEmpty(Identifier))
            {
                return Guid;
            }
            return "["+IdentifierField+"]:[" + Identifier + "]";
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsStringBasedIdentifier;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsStringBasedIdentifier);
        }
    }
}
