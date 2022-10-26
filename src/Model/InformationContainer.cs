using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Container.ExtendedDocument;
using IIB.ICDD.Model.PayloadTriples;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using IIB.ICDD.Validation;
using Newtonsoft.Json.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace IIB.ICDD.Model
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  InformationContainer 
    /// (c) 2022, Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="IRdfFile" />
    public class InformationContainer : IRdfFile
    {
        #region DataMember
        private string _containerName;
        public string ContainerName
        {
            get => _containerName;
            set
            {
                if (value.Split('.').Last().Equals("icdd"))
                {
                    _containerName = value;
                }
                else
                {
                    _containerName = value + ".icdd";
                }

            }
        }
        public string ContainerGuid { get; }
        public Graph IndexGraph { get; }
        public CtContainerDescription ContainerDescription { get; set; }
        public List<IcddOntology> UserDefinedOntologies { get; set; } = new();
        public List<IcddPayloadTriples> PayloadTriples { get; set; } = new();
        public List<CtDocument> Documents => ContainerDescription?.ContainsDocument as List<CtDocument>;
        public List<CtLinkset> Linksets => ContainerDescription?.ContainsLinkset as List<CtLinkset>;
        public List<CtParty> Parties => ContainerDescription?.ContainsParty as List<CtParty>;
        public string PathToContainer { get; private set; }
        public string PathToIndex { get; private set; }
        public InformationContainerRepository Repository { get; private set; }
        public string VersionID => ContainerDescription?.VersionId;
        public string VersionDescription => ContainerDescription?.VersionDescription;
        #endregion

        #region Constructors
        internal InformationContainer(string inputFilePath, bool useCustomWorkfolder = false, string customWorkfolder = "", bool useCustomGuid = false, string customGuid = "")
        {
            //open existing container from file
            ContainerName = Path.GetFileName(inputFilePath);
            ContainerGuid = useCustomGuid ? customGuid : Guid.NewGuid().ToString();
            PathToContainer = useCustomWorkfolder ? FileHandler.GetContainerPath(ContainerGuid, customWorkfolder) : FileHandler.GetContainerPath(ContainerGuid);

            ZipFile.ExtractToDirectory(inputFilePath, PathToContainer);
            PathToIndex = ContainerHandler.Index(PathToContainer);

            try
            {
                IndexGraph = new Graph();
                RdfXmlParser xmlParser = new();
                xmlParser.Load(IndexGraph, GetIndexFilePath());
            }
            catch (Exception e)
            {
                IndexGraph = new Graph();
                Logger.Log("Could not parse IndexRdfGraph(" + e + ")", Logger.MsgType.Error, "IcddContainer()");
            }
            finally
            {
                List<Uri> imports = ContainerHandler.OrganizeImports(IndexGraph, PathToContainer);
                UserDefinedOntologies = ContainerHandler.OrganizeOntologies(imports, GetOntologyFolder());

                ContainerDescription = CtContainerDescription.Read(this, RdfReader.GetInstances(IndexGraph, IcddTypeSpecsHelper.CtContainerDescription).FirstOrDefault());

                if (ContainerDescription != null)
                {
                    ContainerDescription.Modification = DateTime.Now;
                    Logger.Log("Created new CtContainerDescription for " + ContainerName, Logger.MsgType.Info,
                        " CtContainerDescription()");
                }
                PayloadTriples = IcddPayloadTriples.ReadAll(this);
                Repository = InformationContainerRepository.GetForInformationContainer(this);
            }
        }
        internal InformationContainer(string customWorkfolder, string customGuid, string filename)
        {
            ContainerGuid = customGuid;
            ContainerName = filename;
            PathToContainer = Path.Combine(customWorkfolder, ContainerGuid);
            PathToIndex = ContainerHandler.Index(PathToContainer);

            try
            {
                IndexGraph = new Graph();
                RdfXmlParser xmlParser = new();
                xmlParser.Load(IndexGraph, GetIndexFilePath());
            }
            catch (Exception e)
            {
                IndexGraph = new Graph();
                Logger.Log("Could not parse IndexRdfGraph(" + e + ")", Logger.MsgType.Error, "IcddContainer(string guid, string filename)");
            }
            finally
            {
                List<Uri> imports = ContainerHandler.OrganizeImports(IndexGraph, PathToContainer);
                UserDefinedOntologies = ContainerHandler.OrganizeOntologies(imports, GetOntologyFolder());

                ContainerDescription = CtContainerDescription.Read(this, RdfReader.GetInstances(IndexGraph, IcddTypeSpecsHelper.CtContainerDescription).FirstOrDefault());
                if (ContainerDescription != null)
                {
                    ContainerDescription.Modification = DateTime.Now;
                    Logger.Log("Created new CtContainerDescription for " + ContainerName, Logger.MsgType.Info,
                        " CtContainerDescription()");
                }
                PayloadTriples = IcddPayloadTriples.ReadAll(this);
                Repository = InformationContainerRepository.GetForInformationContainer(this);
            }
        }
        internal InformationContainer(IcddContainerBuilderOptions options)
        {
            //Create new empty container
            ContainerName = options.ContainerName;
            ContainerGuid = options.UseCustomGuid ? options.CustomGuid : ContainerHandler.Encode(Guid.NewGuid());
            PathToContainer = options.UseCustomWorkfolder ? FileHandler.GetContainerPath(ContainerGuid, options.CustomWorkfolder) : FileHandler.GetContainerPath(ContainerGuid);
            Directory.CreateDirectory(PathToContainer);
            Directory.CreateDirectory(Path.Combine(PathToContainer, "Ontology Resources"));
            Directory.CreateDirectory(Path.Combine(PathToContainer, "Payload Documents"));
            Directory.CreateDirectory(Path.Combine(PathToContainer, "Payload Triples"));
            FileStream file = File.Create(Path.Combine(PathToContainer, "index.rdf"));
            file.Close();


            PathToIndex = ContainerHandler.Index(PathToContainer);
            IndexGraph = new Graph();
            ContainerHandler.SetBaseNamespace(this, options.Namespace);
            ContainerHandler.CreateImports(this, PathToContainer);
            
            SaveRdf();
            Repository = InformationContainerRepository.GetForInformationContainer(this);
        }

        public InformationContainer()
        {
            //Create new empty container
            ContainerName = "empty.icdd";
            ContainerGuid = Guid.NewGuid().ToString();
            PathToContainer = FileHandler.GetContainerPath(ContainerGuid);
            Directory.CreateDirectory(PathToContainer);
            Directory.CreateDirectory(Path.Combine(PathToContainer, "Ontology Resources"));
            Directory.CreateDirectory(Path.Combine(PathToContainer, "Payload Documents"));
            Directory.CreateDirectory(Path.Combine(PathToContainer, "Payload Triples"));
            FileStream file = File.Create(Path.Combine(PathToContainer, "index.rdf"));
            file.Close();


            PathToIndex = ContainerHandler.Index(PathToContainer);
            IndexGraph = new Graph();
            ContainerHandler.SetBaseNamespace(this);
            ContainerHandler.CreateImports(this, PathToContainer);
            
            SaveRdf();
            Repository = InformationContainerRepository.GetForInformationContainer(this);
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return ContainerName;
        }

        public bool SaveRdf()
        {
            bool result = false;
            try
            {
                if (Linksets != null)
                {
                    foreach (CtLinkset ls in Linksets)
                    {
                        ls.SaveRdf();
                    }
                }

                PrettyRdfXmlWriter rdfxmlwriter = new();
                rdfxmlwriter.Save(IndexGraph, PathToIndex);
                result = true;
            }
            catch (Exception e)
            {
                Logger.Log("Konnte RDF Datei nicht schreiben. Exception: " + e, Logger.MsgType.Error, "SaveIndexRdf()");
            }
            return result;
        }
        #endregion

        #region IoMethods
        public string GetOntologyFolder()
        {
            return Path.Combine(PathToContainer, "Ontology resources/");
        }
        public string GetDocumentFolder()
        {
            return Path.Combine(PathToContainer, "Payload documents/");
        }
        public string GetLinksetFolder()
        {
            return Path.Combine(PathToContainer, "Payload triples/");
        }
        #endregion

        #region ContainerMethods

        public InformationContainer NextVersion(string versionID, string versionDescription, string filename, bool createNewFolder, string newGuid = "")
        {
            SaveRdf();
            if (createNewFolder && !string.IsNullOrEmpty(newGuid))
            {
                var dir = Directory.GetParent(PathToContainer);
                if (dir != null)
                {
                    var newDirectory = Path.Combine(dir.FullName, newGuid);
                    FileHandler.Copy(PathToContainer, newDirectory);

                    var container = new InformationContainer(dir.FullName, newGuid, filename);
                    container.ContainerDescription.NextVersion(versionID, versionDescription);
                    return container;
                }
                return null;
            }
            else
            {
                ContainerDescription.NextVersion(versionID, versionDescription);
                return this;
            }


        }

        public InformationContainer Move(string newDirectory)
        {
            try
            {
                FileHandler.Copy(PathToContainer, newDirectory);
                Directory.Move(PathToContainer + ".icdd", newDirectory + ".icdd");
            }
            catch (Exception e)
            {
                Logger.Log("Could not move container (" + e + ")", Logger.MsgType.Error, "Move()");
                return null;
            }

            PathToContainer = newDirectory;
            PathToIndex = ContainerHandler.Index(PathToContainer);
            SaveRdf();

            return this;
        }

        public InformationContainer Duplicate(string? newContainerId, string? newContainerName)
        {
            var name = (newContainerName == null || newContainerName == "") ? ContainerName : newContainerName;
            var id = (newContainerId == null || newContainerId == "") ? ContainerHandler.Encode(Guid.NewGuid()) : newContainerId;
            var version = 1;

            var duplicateContainer = NextVersion(version.ToString(), $"Version {version} of {name}", name, true, id);

            return duplicateContainer;
        }

        public IEnumerable<string> GetOntologyReferences()
        {
            return Directory.GetFiles(GetOntologyFolder()).ToList().Select(Path.GetFileName);

        }

        internal string GetIndexFilePath()
        {
            return PathToIndex;
        }

        public void Delete()
        {
            var di = new DirectoryInfo(PathToContainer);
            var git = new DirectoryInfo(Path.Combine(PathToContainer, ".git"));
            Repository.Dispose();
            if (git.Exists)
            {
                FileHandler.DeleteReadOnlyDirectory(git.FullName);
            }
            foreach (var file in di.GetFiles())
                file.Delete();
            foreach (var dir in di.GetDirectories())
                dir.Delete(true);
            di.Delete(true);

        }

        #endregion

        #region DocumentMethods
        public bool ContainsDocument(string filename)
        {
            return ContainerDescription.HasDocument(filename);
        }

        public CtDocument GetDocument(string id)
        {
            return ContainerDescription.GetDocument(id);
        }

        public bool DeleteDocument(string id)
        {
            CtDocument document = ContainerDescription.GetDocument(id);
            return document.Delete();
        }

        public CtFolderDocument CreateFolderDocument(string foldername)
        {
            if (Directory.Exists(Path.Combine(GetDocumentFolder(), foldername))) return null;

            Directory.CreateDirectory(Path.Combine(GetDocumentFolder(), foldername));
            var document = new CtFolderDocument(this, foldername, "folder", "folder")
            {
                Description = "This folder has been added using the ICDDToolkitLibrary.",
                VersionId = "1",
                VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
            };
            return ContainerDescription.AddDocument(document) ? document : null;
        }

        public CtSecuredDocument CreateSecuredDocument(string sourceFilePath, string filename, string fileExtension, string format, string checksum, string checksumAlgorithm)
        {
            var destinationFilePath = Path.Combine(GetDocumentFolder() + filename);
            if (!File.Exists(sourceFilePath)) return null;

            if (FileHandler.FileExists(destinationFilePath, out string newDest, out string newFilename))
            {
                destinationFilePath = newDest;
                filename = newFilename;
            }

            File.Copy(sourceFilePath, destinationFilePath);

            var document = new CtSecuredDocument(this, filename, fileExtension,
                format, checksum, checksumAlgorithm)
            {
                Description = "This document has been added using the ICDDToolkitLibrary.",
                VersionId = "1",
                VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
            };
            return ContainerDescription.AddDocument(document) ? document : null;
        }

        public CtEncryptedDocument CreateEncryptedDocument(string sourceFilePath, string filename, string fileExtension, string mimeType, string encryptionAlgorithm)
        {
            var destinationFilePath = Path.Combine(GetDocumentFolder() + filename);
            if (!File.Exists(sourceFilePath)) return null;

            if (FileHandler.FileExists(destinationFilePath, out string newDest, out string newFilename))
            {
                destinationFilePath = newDest;
                filename = newFilename;
            }

            File.Copy(sourceFilePath, destinationFilePath);

            var document = new CtEncryptedDocument(this, filename, fileExtension,
                mimeType, encryptionAlgorithm)
            {
                Description = "This document has been added using the ICDDToolkitLibrary.",
                VersionId = "1",
                VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
            };
            return ContainerDescription.AddDocument(document) ? document : null;
        }

        public CtInternalDocument CreateInternalDocument(string sourceFilePath, string filename, string fileExtension, string mimeType, string folderID = null)
        {
            var folder = this.GetDocument(folderID) as CtFolderDocument;
            var destinationFilePath = folder == null ? Path.Combine(GetDocumentFolder(), filename) : Path.Combine(GetDocumentFolder(), folder.Foldername, filename);
            if (!File.Exists(sourceFilePath)) return null;

            if (FileHandler.FileExists(destinationFilePath, out string newDest, out string newFilename))
            {
                destinationFilePath = newDest;
                filename = newFilename;
            }
            filename = folder == null ? filename : Path.Combine(folder.Foldername, filename);
            File.Copy(sourceFilePath, destinationFilePath);
            var document =
                new CtInternalDocument(this, filename, fileExtension, mimeType)
                {
                    Description = "This document has been added using the ICDDToolkitLibrary.",
                    VersionId = "1",
                    VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
                };
            return ContainerDescription.AddDocument(document) ? document : null;
        }

        public CtInternalDocument CreateInternalDocument(string filename, string fileExtension, string mimeType)
        {
            var destinationFilePath = Path.Combine(GetDocumentFolder(), filename);


            if (FileHandler.FileExists(destinationFilePath, out string newDest, out string newFilename))
            {
                destinationFilePath = newDest;
                filename = newFilename;
            }
            var document =
                new CtInternalDocument(this, filename, fileExtension, mimeType)
                {
                    Requested = true,
                    Description = "This document has been added using the ICDDToolkitLibrary.",
                    VersionId = "1",
                    VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
                };
            return ContainerDescription.AddDocument(document) ? document : null;
        }

        public IcddOntology CreateOntology(string sourceFilePath)
        {
            var filename = Path.GetFileName(sourceFilePath);
            var destinationFilePath = Path.Combine(GetOntologyFolder(), filename);
            if (!File.Exists(sourceFilePath)) return null;

            if (FileHandler.FileExists(destinationFilePath, out string newDest, out string newFilename))
            {
                destinationFilePath = newDest;
            }
            File.Copy(sourceFilePath, destinationFilePath);
            File.Delete(sourceFilePath);
            List<Uri> imports = ContainerHandler.OrganizeImports(IndexGraph, PathToContainer);
            UserDefinedOntologies = ContainerHandler.OrganizeOntologies(imports, GetOntologyFolder());
            return new IcddOntology(destinationFilePath);
        }

        public bool DeleteOntology(string filename)
        {
            IcddOntology onto = UserDefinedOntologies.Find(ont => ont.GetFileName() == filename);
            if (onto == null) return false;

            File.Delete(Path.Combine(GetOntologyFolder(), filename));
            return UserDefinedOntologies.Remove(onto);
        }

        public IcddPayloadTriples CreatePayloadTriple(string sourceFilePath, bool overwrite = false)
        {
            var filename = Path.GetFileName(sourceFilePath);
            var destinationFilePath = Path.Combine(GetLinksetFolder(), filename);
            if (!File.Exists(sourceFilePath) && !FileHandler.IsRdf(sourceFilePath)) return null;

            if (!overwrite && FileHandler.FileExists(destinationFilePath, out string newDest, out string newFilename))
            {
                destinationFilePath = newDest;
            }
            File.Copy(sourceFilePath, destinationFilePath, overwrite);
            PayloadTriples = IcddPayloadTriples.ReadAll(this);
            return PayloadTriples.Find(trip => trip.GetFileName() == filename);
        }

        public bool DeletePayloadTriple(string filename)
        {
            IcddPayloadTriples onto = PayloadTriples.Find(ont => ont.GetFileName() == filename);
            return onto != null && onto.Delete();
        }

        public CtExternalDocument CreateExternalDocument(string documentUri, string name, string fileExtension, string mimeType)
        {
            var document = new CtExternalDocument(this, documentUri, name, fileExtension, mimeType)
            {
                Description = "This document has been added using the ICDDToolkitLibrary.",
                VersionId = "1",
                VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
            };
            return ContainerDescription.AddDocument(document) ? document : null;
        }

        public CtExternalDocument CreateDatabaseLink(string connectionString, string databaseName, string databaseType, string queryLanguage, CtDocument mappingFile = null)
        {
            var document = new ExtDatabaseLink(this, connectionString, databaseName, databaseType, queryLanguage, mappingFile)
            {
                Description = "This database has been added using the ICDDToolkitLibrary.",
                VersionId = "1",
                VersionDescription = "This is the first version added using the ICDDToolkitLibrary."
            };
            return ContainerDescription.AddDocument(document) ? document : null;
        }

        public bool AddDocument(CtDocument document)
        {
            return ContainerDescription.AddDocument(document);
        }
        #endregion

        #region LinksetMethods

        public CtLinkset CreateLinkset(string filename)
        {
            var linkset = new CtLinkset(ContainerDescription.Parent, null, filename);
            ContainerDescription.AddLinkset(linkset);
            return linkset;
        }
        public bool AddLinkset(CtLinkset linkset)
        {
            return ContainerDescription.AddLinkset(linkset);
        }

        public CtLinkset GetLinkset(string id)
        {
            return ContainerDescription.GetLinkset(id);
        }

        public bool DeleteLinkset(string id)
        {
            CtLinkset linkset = ContainerDescription.GetLinkset(id);
            return linkset != null && linkset.Delete();
        }

        #endregion

        #region JsonLD

        public IcddResourceWrapper CreateElementFromJsonLD(JObject inputData)
        {
            JsonLdParser parser = new();
            TripleStore store = new();
            StringReader sr = new(inputData.ToString());
            parser.Load(store, sr);

            Graph g = new();
            g.Assert(store.Triples);

            IcddShaclValidator val = new(g);
            if (!val.Conforms()) return null;


            IcddResourceWrapper predicate = new(IcddNamespacesHelper.RDF_URI.AbsoluteUri, "type", IndexGraph);
            IEnumerable<Triple> triples = g.GetTriplesWithPredicate(predicate.AsNode()).ToList();

            if (IndexGraph.ContainsTriple(triples.FirstOrDefault()))
            {
                return null;
            }
            IUriNode resource = triples.FirstOrDefault()?.Subject as IUriNode;
            IUriNode objectNode = triples.FirstOrDefault()?.Object as IUriNode;
            string typeElement = new IcddResourceWrapper(objectNode).Identifier;
            IEnumerable<Triple> relatedTriples = g.GetTriplesWithSubject(resource);
            IndexGraph.Assert(relatedTriples);

            switch (typeElement)
            {
                case IcddTypeSpecsHelper.sCtPerson:
                    Parties.Add(CtPerson.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtOrganisation:
                    Parties.Add(CtOrganisation.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtContainerDescription:
                    ContainerDescription = CtContainerDescription.Read(this, resource);
                    break;
                case IcddTypeSpecsHelper.sCtEncryptedDocument:
                    AddDocument(CtEncryptedDocument.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtExternalDocument:
                    AddDocument(CtExternalDocument.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtInternalDocument:
                    AddDocument(CtInternalDocument.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtDocument:
                    AddDocument(CtDocument.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtSecuredDocument:
                    AddDocument(CtSecuredDocument.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtFolderDocument:
                    AddDocument(CtFolderDocument.Read(this, resource));
                    break;
                case IcddTypeSpecsHelper.sCtLinkset:
                    AddLinkset(CtLinkset.Read(this, resource));
                    break;
                default:
                    break;
            }
            IcddResourceWrapper ge = new(objectNode);
            return ge;

        }

        #endregion

    }
}