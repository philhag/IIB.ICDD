using System;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container.ExtendedDocument
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  ExtDatabaseLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class ExtPayloadProxy : CtExternalDocument
    {
        public Uri BaseUri
        {
            get => new(Url);
            set => Url = value.AbsoluteUri;
        }



        internal ExtPayloadProxy(InformationContainer container, Uri baseUri, string filename)
            : base(container, baseUri.AbsoluteUri, filename, "http", "rfc3986")
        {
        }

        private ExtPayloadProxy(InformationContainer container, INode resource) : base(container, resource)
        {
        }

        public new static ExtPayloadProxy Read(InformationContainer container, INode resource)
        {
            try
            {
                return new ExtPayloadProxy(
                    container ?? throw new ArgumentNullException(nameof(container)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logger.Log("Argument Exception: " + exception, Logger.MsgType.Warning, "ExtPayloadProxy.Read");
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
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "[Proxy] " + Name;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.ExtDocPayloadProxy;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.ExtDocPayloadProxy);
        }
    }
}