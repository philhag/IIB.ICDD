using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container.Document
{

    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtSecuredDocument 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="CtInternalDocument" />

    public class CtSecuredDocument : CtInternalDocument
    {

        public string Checksum
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.CHECKSUM);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.CHECKSUM, value, AttributeType.String);

        }
        public string ChecksumAlgorithm
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.CHECKSUM_ALGORITHM);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.CHECKSUM_ALGORITHM, value, AttributeType.String);

        }

        internal CtSecuredDocument( InformationContainer container, string filename, string filetype, string format, string checksum, string checksumAlgorithm) : 
            base( container, filename, filetype, format)
        {
            Checksum = checksum;
            ChecksumAlgorithm = checksumAlgorithm;
        }

        private CtSecuredDocument( InformationContainer container, INode resource) : base(container, resource)
        {
        }

        public new static CtSecuredDocument Read(InformationContainer container, INode resource)
        {
            try
            {
                return new CtSecuredDocument(
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
            return IcddTypeSpecsHelper.CtSecuredDocument;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtSecuredDocument);
        }
    }
}
