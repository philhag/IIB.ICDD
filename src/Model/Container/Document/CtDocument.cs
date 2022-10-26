using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IIB.ICDD.Handling;
using IIB.ICDD.Model.DynamicSemantics.Properties;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Model.Linkset.Link;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container.Document
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtDocument
    /// abstract class for references to a document; an individual shall at least be member of ct:ExternalDocument or ct:InternalDocument; and optionally, individuals can be a member of other subtypes of ct:Document such as ct:SecuredDocument and/or ct:EncryptedDocument
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>


    public abstract class CtDocument : DsComplexPropertyValue, IIcddVersioning
    {
        /// <summary>
        /// a boolean to indicate whether a document is required or not. When this property is not set the value can be interpreted as 'false'
        /// </summary>
        public bool Requested
        {
            get => RdfReader.GetDataPropertyAsBool(Graph, this, IcddPredicateSpecsHelper.IS_REQUESTED);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.IS_REQUESTED, value, AttributeType.Bool);
        }

        /// <summary>
        /// an optional character string that may be used to identify a version of the corresponding resource
        /// </summary>
        public string VersionId
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.VERSION_ID);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.VERSION_ID, value, AttributeType.String);
        }

        /// <summary>
        /// an optional character string that may be used to provide a description for a version of the corresponding resource 
        /// </summary>
        public string VersionDescription
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.VERSION_DESCRIPTION);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.VERSION_DESCRIPTION, value, AttributeType.String);
        }

        /// <summary>
        /// a name for a resource
        /// </summary>
        public string Name
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.NAME);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.NAME, value, AttributeType.String);

        }

        /// <summary>
        /// a general description
        /// </summary>
        public string Description
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.DESCRIPTION);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.DESCRIPTION, value, AttributeType.String);

        }

        /// <summary>
        /// a string that specifies the file type such as  "GML", "IFC", "shp", "xlsx", "pdf","rvt"; the string may be a compound string in indicating also version and data format (e.g. "ifc-4-xml-zip")
        /// </summary>
        public string FileType
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.FILE_TYPE);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FILE_TYPE, value, AttributeType.String, true);
        }

        /// <summary>
        /// The mediatype of a document following the Internet Assigned Numbers Authority's specification (https://www.iana.org/assignments/media-types/media-types.xhtml);examples are 'application/pdf' and 'audio/mpeg'
        /// </summary>
        public string FileFormat
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.FILE_FORMAT);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FILE_FORMAT, value, AttributeType.String, true);

        }

        /// <summary>
        /// a property to link a document to an alternative version of that document
        /// </summary>
        public CtDocument AlternativeDocument
        {
            get => Read(Container, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.ALTERNATIVE_DOCUMENT)?.AsNode());
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.ALTERNATIVE_DOCUMENT, value.AsUri(), AttributeType.Uri, true);

        }

        /// <summary>
        /// an optional reference to the prior version of this resource
        /// </summary>
        public new CtDocument PriorVersion
        {
            get => Read(Container, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.PRIOR_VERSION)?.AsNode());
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.PRIOR_VERSION, value.AsUri(), AttributeType.Uri, true);

        }

        /// <summary>
        /// a property defining the relation between a document reference and a container
        /// </summary>
        protected CtContainerDescription BelongsToContainer { get; set; }

        protected CtDocument(InformationContainer container, INode resource) : base(container.IndexGraph, container, resource)
        {
        }

        protected CtDocument(InformationContainer container) : base(container.IndexGraph, container)
        {
        }

        /// <summary>
        /// Method that reads a Document from an Information container and a specific RDF resource, and initializes the protected constructor
        /// </summary>
        /// <param name="container"></param>
        /// <param name="resource"></param>
        /// <returns>The initialized document</returns>
        public static CtDocument Read(InformationContainer container, INode resource)
        {
            return RdfReader.GetDocument(container, resource);
        }

        /// <summary>
        /// Gets all LinkElements that are linked with this document
        /// </summary>
        /// <returns></returns>
        public List<LsLinkElement> GetLinkedElements()
        {
            return BelongsToContainer.GetLinkElementsByDocument(this);
        }

        /// <summary>
        /// Deletes this document
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            if (!BelongsToContainer.Parent.ContainsDocument(Name))
                return false;


            if (ContainerHandler.IsSameOrSubclass(typeof(CtInternalDocument), this.GetType()) && !this.Requested)
            {
                if (ContainerHandler.IsSameOrSubclass(typeof(CtFolderDocument), this.GetType()))
                {
                    if (!Directory.Exists(AbsoluteFilePath()))
                    {
                        return false;
                    }
                    else
                    {
                        try
                        {
                            var folder = (this as CtFolderDocument);
                            foreach(var document in folder.GetDocuments())
                            {
                                (document as CtInternalDocument).MoveToFolder(null);
                            }
                            Directory.Delete(AbsoluteFilePath(), true);
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (!File.Exists(AbsoluteFilePath()))
                    {
                        return false;
                    }

                    try
                    {
                        File.Delete(AbsoluteFilePath());
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            var linksPerLinkset = this.GetLinks();
            foreach (CtLinkset ls in linksPerLinkset.Keys)
            {
                ls.DeleteLink(linksPerLinkset[ls]);
            }
            return RdfWriter.DeleteDocument(Graph, this) && BelongsToContainer.ContainsDocument.Remove(this);
        }

        /// <summary>
        /// Returns a string representation of the document
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "File [" + Name + "(" + FileType + ", " + VersionId + "): " + Description + "]";
        }

        /// <summary>
        /// overrides standard equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return base.Equals(obj);
            }
            else
            {
                CtDocument p = (CtDocument)obj;
                bool notPrimitiveEqual = true;
                if (AlternativeDocument != null) { notPrimitiveEqual &= AlternativeDocument.Equals(p.AlternativeDocument); }
                if (PriorVersion != null) { notPrimitiveEqual &= PriorVersion.Equals(p.PriorVersion); }

                return base.Equals(obj) && notPrimitiveEqual
                    && (Name == p.Name)
                    && (Description == p.Description)
                    && (FileType == p.FileType)
                    && (FileFormat == p.FileFormat)
                    && (Requested == p.Requested);
            }
        }


        /// <summary>
        /// Returns the URI of the Element type
        /// </summary>
        /// <returns></returns>
        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtDocument;
        }

        /// <summary>
        /// Returns the RDF resource of the element type
        /// </summary>
        /// <returns></returns>
        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtDocument);
        }

        /// <summary>
        /// Gets the Hash code of the element
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Gets the file path of the document
        /// </summary>
        /// <returns></returns>
        public abstract string AbsoluteFilePath();

        /// <summary>
        /// Gets all links in all linksets involving this document
        /// </summary>
        /// <returns></returns>
        public Dictionary<CtLinkset, List<LsLink>> GetLinks()
        {
            return Container.Linksets.ToDictionary(linkSet => linkSet, linkSet => linkSet.GetLinksFor(this));
        }

        /// <summary>
        /// Creates a new version of this document that can optionally be copied to another container
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="versionDescription"></param>
        /// <param name="copyToContainer"></param>
        /// <returns></returns>
        public IcddBaseElement NextVersion(string versionID, string versionDescription, InformationContainer copyToContainer = null)
        {
            var versionNr = -1;
            if (int.TryParse(versionID, out var newVersion) && int.TryParse(VersionId, out var oldVersion) &&
                oldVersion < newVersion)
            {
                versionNr = newVersion;
            }
            else
                _ = int.TryParse(versionID, out versionNr);

            if (copyToContainer != null)
            {
                var document = copyToContainer.CreateInternalDocument(this.AbsoluteFilePath(), this.Name, this.FileFormat,
                    this.FileType);
                document.VersionId = versionNr > -1 ? versionNr.ToString() : versionID;
                document.VersionDescription = "Version of " + this.Name + " versioned at " +
                                              DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                return document;
            }
            else
            {
                this.VersionId = versionNr > -1 ? versionNr.ToString() : versionID;
                this.VersionDescription = "Version of " + this.Name + " versioned at " +
                                          DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                return this;
            }
        }
    }
}