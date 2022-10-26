using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container.Document
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtEncryptedDocument 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="CtInternalDocument" />

    public class CtEncryptedDocument : CtInternalDocument
    {
        public string EncryptionAlgorithm
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.ENCRYPTION_ALGORITHM);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.ENCRYPTION_ALGORITHM, value, AttributeType.String);

        }

        internal CtEncryptedDocument(InformationContainer container, string filename, string filetype, string format, string encryptionAlgorithm) :
            base(container, filename, filetype, format)
        {
            EncryptionAlgorithm = encryptionAlgorithm;
        }

        private CtEncryptedDocument(InformationContainer container, INode resource) : base(container, resource)
        {
        }

        public new static CtEncryptedDocument Read(InformationContainer container, INode resource)
        {
            try
            {
                return new CtEncryptedDocument(
                container ?? throw new ArgumentNullException(nameof(container)),
                resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtOrganisation.Read");
                return null;
            }
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtEncryptedDocument;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtEncryptedDocument);
        }
    }
}
