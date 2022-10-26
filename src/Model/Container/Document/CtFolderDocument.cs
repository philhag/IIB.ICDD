using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IIB.ICDD.Handling;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container.Document
{

    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtFolderDocument 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="CtInternalDocument" />

    public class CtFolderDocument : CtInternalDocument
    {
        public string Foldername
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.FOLDER_NAME);
            set { 
                
                RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FOLDER_NAME, value, AttributeType.String);
                foreach (var document in Documents)
                {
                    (document as CtInternalDocument).MoveToFolder(this);
                }
            }
        }

        private List<CtDocument> Documents
        {
            get => Container.Documents.FindAll(document => document is CtInternalDocument && document.Name.Equals(Foldername + "/" + document.Name.Split("/").Last()));
        }

        internal CtFolderDocument(InformationContainer container, string folderName, string filetype, string format) : base(container, folderName, filetype, format)
        {
            Foldername = folderName;
            try
            {
                Directory.CreateDirectory(GetFolderPath());
            }
            catch (Exception e)
            {
                throw new IcddException("Could not generate folder document.", e);
            }
        }

        private CtFolderDocument(InformationContainer container, INode resource) : base(container, resource)
        {
            if (!Directory.Exists(GetFolderPath()))
            {
                try
                {
                    Directory.CreateDirectory(GetFolderPath());
                }
                catch (Exception e)
                {
                    throw new IcddException("Could not generate folder document.", e);
                }
            }
        }

        public new static CtFolderDocument Read(InformationContainer container, INode resource)
        {
            try
            {
                return new CtFolderDocument(
                  container ?? throw new ArgumentNullException(nameof(container)),
                  resource ?? throw new ArgumentNullException(nameof(resource)));

            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtFolderDocument.Read");
                return null;
            }



        }

        public string GetFolderPath()
        {
            return Path.Combine(Container.GetDocumentFolder(), Foldername);
        }

        public List<CtDocument> GetDocuments()
        {
            return Documents;
        }

        public bool IsEmpty()
        {
            return !GetDocuments().Any();
        }

        public override bool MoveToFolder(CtFolderDocument folder)
        {
            var currentFileName = Foldername;
            if (folder == null)
            {
                try
                {
                    Foldername = currentFileName.Split("/").Last();
                    return true;
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
                    Foldername = folder.FileName + "/" + currentFileName.Split("/").Last();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtFolderDocument;
        }
        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtFolderDocument);
        }
    }
}
