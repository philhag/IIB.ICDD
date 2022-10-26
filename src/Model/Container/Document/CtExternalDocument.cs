using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container.Document
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtExternalDocument 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class CtExternalDocument : CtDocument
    {
        public string Url
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.URL);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.URL, value, AttributeType.String);
        }

        internal CtExternalDocument(InformationContainer container, string url, string name, string filetype, string format) : base(container)
        {
            BelongsToContainer = container.ContainerDescription;
            Url = url;
            Name = name;
            FileFormat = format;
            FileType = filetype;
        }

        protected CtExternalDocument(InformationContainer container, INode resource) : base(container, resource)
        {
            BelongsToContainer = container.ContainerDescription;
        }

        public new static CtExternalDocument Read(InformationContainer container, INode resource)
        {
            try
            {
                return new CtExternalDocument(
                    container ?? throw new ArgumentNullException(nameof(container)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtExternalDocument.Read");
                return null;
            }
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return base.Equals(obj);
            }
            else
            {
                CtExternalDocument p = (CtExternalDocument)obj;
                return base.Equals(obj) && (Url == p.Url);
            }
        }

        protected bool Equals(CtExternalDocument other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string AbsoluteFilePath()
        {
            return Url;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtExternalDocument;
        }
        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtExternalDocument);
        }
    }
}