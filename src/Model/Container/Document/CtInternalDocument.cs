using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container.Document
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtInternalDocument 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de
    /// </summary>

    public class CtInternalDocument : CtDocument
    {
        public IcddOntology OntologicalView;
        public List<IcddOntology> ReferenceOntologies = new();
        public string FileName
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.FILE_NAME);
            set
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FILE_NAME, value, AttributeType.String, true);
                }
                else
                {
                    var locationOld = AbsoluteFilePath();
                    var locationNew = Path.Combine(Container.GetDocumentFolder(), value);

                    if (File.Exists(locationOld) && !File.Exists(locationNew) && Path.IsPathFullyQualified(locationNew))
                    {
                        File.Move(locationOld, locationNew);
                        if (!File.Exists(locationNew)) return;
                        Name = value;
                        RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FILE_NAME, value, AttributeType.String, true);
                    }
                    else
                    {
                        if (!File.Exists(locationNew)) return;
                        Name = value;
                        RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FILE_NAME, value, AttributeType.String, true);
                    }
                }
            }
        }
        internal CtInternalDocument(InformationContainer container, string filename, string filetype, string format) : base(container)
        {
            Name = filename;
            FileName = filename;
            FileType = filetype;
            FileFormat = format;
            BelongsToContainer = container.ContainerDescription;

            if (FileType.Contains("ttl") || FileType.Contains("rdf"))
            {
                OntologicalView = new IcddOntology(AbsoluteFilePath());

                foreach (var ns in OntologicalView.Namespaces.Prefixes)
                {
                    var uri = OntologicalView.Namespaces.GetNamespaceUri(ns);
                    if (uri != null)
                    {
                        var ontol = Container.UserDefinedOntologies.Find(onto => onto.BaseUri == uri);
                        if (ontol == null) continue;
                        ReferenceOntologies.Add(ontol);
                    }
                }
            }
        }

        protected CtInternalDocument(InformationContainer container, INode resource) : base(container, resource)
        {
            BelongsToContainer = container.ContainerDescription;
            if (FileType.Contains("ttl") || FileType.Contains("rdf"))
            {
                OntologicalView = new IcddOntology(AbsoluteFilePath());

                foreach (var ns in OntologicalView.Namespaces.Prefixes)
                {
                    var uri = OntologicalView.Namespaces.GetNamespaceUri(ns);
                    if (uri != null)
                    {
                        var ontol = Container.UserDefinedOntologies.Find(onto => onto.BaseUri == uri);
                        if (ontol == null) continue;
                        ReferenceOntologies.Add(ontol);
                    }
                }
            }
        }

        public new static CtInternalDocument Read(InformationContainer container, INode resource)
        {
            try
            {
                return new CtInternalDocument(
                  container ?? throw new ArgumentNullException(nameof(container)),
                  resource ?? throw new ArgumentNullException(nameof(resource)));

            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtInternalDocument.Read");
                return null;
            }

        }

        public CtFolderDocument GetContainingFolder()
        {
            return (CtFolderDocument)Container.Documents.Find(document => document is CtFolderDocument && (document as CtFolderDocument).GetDocuments().Exists(doc => doc.Equals(this)));
        }

        public virtual bool MoveToFolder(CtFolderDocument folder = null)
        {
            var currentFileName = FileName;
            if (folder == null)
            {
                try
                {
                    if (File.Exists(AbsoluteFilePath()))
                    {
                        FileName = currentFileName.Split("/").Last();
                        File.Move(AbsoluteFilePath(),Path.Combine(Container.GetDocumentFolder(), FileName));
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    if (File.Exists(AbsoluteFilePath()))
                    {
                        FileName = folder.FileName + "/" + currentFileName.Split("/").Last();
                        File.Move(AbsoluteFilePath(), Path.Combine(Container.GetDocumentFolder(), FileName));
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        public override string AbsoluteFilePath()
        {
            return Path.Combine(Container.GetDocumentFolder(), FileName);
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtInternalDocument;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtInternalDocument);
        }
    }
}
