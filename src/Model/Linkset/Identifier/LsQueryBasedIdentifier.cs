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
    /// Class:  LsQueryBasedIdentifier 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="LsIdentifier" />
    public class LsQueryBasedIdentifier : LsIdentifier
    {
        public string QueryExpression
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.QUERY_EXPRESSION);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.QUERY_EXPRESSION, value, AttributeType.String);

        }

        public string QueryLanguage
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.QUERY_LANGUAGE);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.QUERY_LANGUAGE, value, AttributeType.String);

        }
        internal LsQueryBasedIdentifier(CtLinkset linkset, string exp, string lang) :
            base(linkset)
        {
            QueryExpression = exp;
            QueryLanguage = lang;
        }

        private LsQueryBasedIdentifier(CtLinkset linkset, INode resource) : base(linkset, resource)
        {
        }

        public static LsQueryBasedIdentifier Read(CtLinkset linkset, INode resource)
        {
            try
            {
                return new LsQueryBasedIdentifier(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "LsQueryBasedIdentifier.Read");
                return null;
            }
        }

        public override string ToString()
        {
            return QueryLanguage + "(" + QueryExpression + ")";
        }


        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsQueryBasedIdentifier;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsQueryBasedIdentifier);
        }
    }
}
