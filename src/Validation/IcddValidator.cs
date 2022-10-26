using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace IIB.ICDD.Validation
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddValidator 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddValidator
    {
        protected InformationContainer Container;
        protected ReadOnlyCollection<ZipArchiveEntry> ContainerEntries;
        protected ZipArchive ContainerFile;
        protected string ContainerFileName, ContainerFilePath, LogPath, WorkFolder;
        protected CultureInfo Culture = new("en-US");
        protected List<IcddValidationResult> ValidationResults = new();




        public IcddValidator(string inputFilePath)
        {
            ContainerFilePath = inputFilePath;
            ContainerFileName = Path.GetFileName(inputFilePath);
            WorkFolder = FileHandler.GetWorkFolder();
            //string webPath = _containerFilePath.Split("\\").Last();
            var datestring = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
            var nm = new Random().Next(150);
            LogPath = Path.Combine(FileHandler.GetLoggingFolder(), "IcddValidationLog-" + datestring + "-" + ContainerFileName.Split('.').First() + nm + ".log");
        }

        public IcddValidator(InformationContainer containerInput)
        {
            Container = containerInput;
            ContainerFileName = containerInput.ContainerName;
            WorkFolder = Directory.GetParent(containerInput.PathToContainer)?.FullName;
            //string webPath = _containerFilePath.Split("\\").Last();
            var datestring = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
            var nm = new Random().Next(150);
            LogPath = Path.Combine(FileHandler.GetLoggingFolder(), "IcddValidationLog-" + datestring + "-" + ContainerFileName.Split('.').First() + nm + ".log");
        }


        public List<IcddValidationResult> Validate()
        {

            IcddValidationLogger.Initialize(LogPath);
            IcddValidationLogger.Log("Validation requested for: " + ContainerFileName);
            if (Container != null)
            {
                //Check IndexFile
                CheckHeaderRdfSpec();
                CheckHeaderRdfOntologyReference();

                //Check whether containe actually contains the documents defined in the header file
                CheckContainerContainsAllDocuments();
                CheckIfPayloadDocumentsContained();
                //CheckIfLinksetsContained();

                //Check ConformanceIndicator
                CheckConformanceIndicator();

                //check linksets
                foreach (var linkset in Container.ContainerDescription.ContainsLinkset)
                {
                    CheckLinksetRdf(linkset);
                    CheckIfLinksetExists(linkset);
                    CheckLinksetRdfOntologyReference(linkset);
                }

                //Check Part2
                //CheckConformanceIndicatorPart2();
            }
            else
            {
                //Check Archive
                CheckAndAppendArchive(ContainerFilePath);
                //Check Index.rdf
                if (CheckHeaderFileExists())
                {
                    //CheckFolders
                    CheckFolder("Ontology Resources");
                    CheckFolder("Payload Documents");
                    CheckFolder("Payload Triples");

                    //Check file extension icdd
                    CheckExtension();

                    //create container for metadata                
                    Container = new InformationContainer(ContainerFilePath);

                    //Check IndexFile
                    CheckHeaderRdfSpec();
                    CheckHeaderRdfOntologyReference();

                    //Check whether containe actually contains the documents defined in the header file
                    CheckContainerContainsAllDocuments();
                    CheckIfPayloadDocumentsContained();
                    //CheckIfLinksetsContained();

                    //Check ConformanceIndicator
                    CheckConformanceIndicator();

                    //check linksets
                    foreach (var linkset in Container.ContainerDescription.ContainsLinkset)
                    {
                        CheckLinksetRdf(linkset);
                        CheckIfLinksetExists(linkset);
                        CheckLinksetRdfOntologyReference(linkset);
                    }

                    //Check Part2
                    //CheckConformanceIndicatorPart2();


                    IcddShaclValidator val = new(Container);
                    //ValidationResults.AddRange(val.Validate());

                }
                else
                {
                    IcddValidationLogger.Log("ERROR: Validation canceled due to missing header file!");
                }
            }

            //save log after validation
            var nErrors = GetNumberOfValidationErrors();
            if (nErrors != 0)
                if (nErrors == 1)
                    IcddValidationLogger.Log(
                        "Your file has failed validation. There is one error that can be found above.");
                else
                    IcddValidationLogger.Log("Your file has failed validation. There are " + nErrors +
                                             " errors that can be found above.");
            else
                IcddValidationLogger.Log("Congratulations. Your file has been validated successfully.");
            IcddValidationLogger.Save();

            return ValidationResults;
        }

        #region Part1Methods

        #region ContainerStructure

        private void CheckAndAppendArchive(string filePath)
        {
            var zipResult = new IcddValidationResult("CONFORMITY: Is valid ZIP file?",
                IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Container);
            try
            {
                using (ContainerFile = ZipFile.OpenRead(filePath))
                {
                    ContainerEntries = ContainerFile.Entries;
                    zipResult.ExaminedValue = true;
                    ValidationResults.Add(zipResult);
                    IcddValidationLogger.Log(zipResult.ToString());
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                zipResult.ExaminedValue = false;
            }
            ValidationResults.Add(zipResult);
            IcddValidationLogger.Log(zipResult.ToString());
        }

        private bool CheckHeaderFileExists()
        {
            var headerResult =
                new IcddValidationResult("CONFORMITY: Has Index.rdf header file?", IcddValidationResult.ValidBoolResult,
                    ValidationGroup.Part1Container);
            try
            {
                var headerFile = ContainerFile.GetEntry("index.rdf");
                if (headerFile != null)
                {
                    headerResult.ExaminedValue = true;
                    ValidationResults.Add(headerResult);
                    IcddValidationLogger.Log(headerResult.ToString());
                    return true;
                }
                headerResult.ExaminedValue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                headerResult.ExaminedValue = false;
            }
            ValidationResults.Add(headerResult);
            IcddValidationLogger.Log(headerResult.ToString());
            return false;
        }

        private void CheckExtension()
        {
            var extensionResult =
                new IcddValidationResult("CONFORMITY: Has *.icdd file extension?", "icdd", ValidationGroup.Part1Container)
                {
                    ExaminedValue = ContainerFileName.Split('.').Last()
                };
            ValidationResults.Add(extensionResult);
            IcddValidationLogger.Log(extensionResult.ToString());
        }

        private void CheckFolder(string folder)
        {
            var folderResult =
                new IcddValidationResult("CONFORMITY: Has " + folder + " folder?",
                    IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Container)
                { ExaminedValue = false };

            foreach (var entry in ContainerEntries)
                if (Culture.CompareInfo.IndexOf(entry.FullName, folder, CompareOptions.IgnoreCase) >= 0)
                {
                    folderResult.ExaminedValue = true;
                    ValidationResults.Add(folderResult);
                    IcddValidationLogger.Log(folderResult.ToString());
                    return;
                }
            ValidationResults.Add(folderResult);
            IcddValidationLogger.Log(folderResult.ToString());
        }

        #endregion

        #region HeaderFile

        private void CheckHeaderRdfSpec()
        {
            var headerResult =
                new IcddValidationResult("CONFORMITY: Complies with RDF/OWL specification?", IcddValidationResult.ValidBoolResult,
                    ValidationGroup.Part1Index);
            var headerResult2 =
                new IcddValidationResult("CONFORMITY: Is Rdf/XML-Abbreviated serialised?", IcddValidationResult.ValidBoolResult,
                    ValidationGroup.Part1Index);
            try
            {
                IGraph g = new Graph();
                var xmlParser = new RdfXmlParser();
                xmlParser.Load(g, Container.GetIndexFilePath());

                var triples = g.Triples;
                var prefixes = g.NamespaceMap;
                if (triples != null && triples.Count != 0 && prefixes != null && prefixes.Prefixes.Count() != 0)
                {
                    headerResult.ExaminedValue = true;
                    headerResult2.ExaminedValue = true;
                    ValidationResults.Add(headerResult);
                    ValidationResults.Add(headerResult2);
                    IcddValidationLogger.Log(headerResult.ToString());
                    IcddValidationLogger.Log(headerResult2.ToString());

                    //foreach (var triple in triples)
                    //{
                    //Log(triple.ToString());
                    //}
                    return;
                }
                headerResult.ExaminedValue = false;
                headerResult2.ExaminedValue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                headerResult.ExaminedValue = false;
                headerResult2.ExaminedValue = false;
            }
            ValidationResults.Add(headerResult);
            IcddValidationLogger.Log(headerResult.ToString());
            ValidationResults.Add(headerResult2);
            IcddValidationLogger.Log(headerResult2.ToString());
        }

        private void CheckHeaderRdfOntologyReference()
        {
            var headerResult =
                new IcddValidationResult("CONFORMITY: Refers directly or indirectly to the Container.rdf ontology?",
                    IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Index);
            try
            {
                var graph = Container.IndexGraph;
                var imports = RdfReader.GetImports(graph);

                if (imports.Any() && (imports.Contains(IcddNamespacesHelper.CONTAINER_ONTOLOGY) || imports.Contains(IcddNamespacesHelper.CONTAINER_URI_DRAFT)))
                {
                    headerResult.ExaminedValue = true;
                    ValidationResults.Add(headerResult);
                    IcddValidationLogger.Log(headerResult.ToString());
                    return;
                }
                else
                {
                    headerResult.ExaminedValue = false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                headerResult.ExaminedValue = false;
            }
            ValidationResults.Add(headerResult);
            IcddValidationLogger.Log(headerResult.ToString());
        }

        private void CheckIfPayloadDocumentsContained()
        {
            var nNotContained = 0;
            var files = Directory.GetFiles(Container.GetDocumentFolder());
            foreach (var doc in files)
            {
                var fileName = doc.Split('/').Last();


                var fileResult =
                    new IcddValidationResult(
                            "CONFORMITY: Is file  '" + fileName +
                            "' from the 'Payload documents' folder listed in the header file?",
                            IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Index)
                    { ExaminedValue = false };
                if (!Container.ContainerDescription.HasDocument(fileName))
                {
                    nNotContained++;
                    IcddValidationLogger.Log(fileResult.ToString());
                    ValidationResults.Add(fileResult);
                }
                else
                {
                    fileResult.ExaminedValue = true;
                    IcddValidationLogger.Log(fileResult.ToString());
                }
            }
            if (nNotContained == 0)
            {
                var result = new IcddValidationResult(
                        "CONFORMITY: Are all files within the 'Payload documents' folder listed in the header file?",
                        IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Index)
                { ExaminedValue = true };
                ValidationResults.Add(result);
                IcddValidationLogger.Log(result.ToString());
            }
        }

        private void CheckConformanceIndicator()
        {
            var headerResult =
                new IcddValidationResult("CONFORMITY: Contains the value 'part1' for the ct:conformanceIndicator property?",
                    IcddConformanceTypesHelper.Part1, ValidationGroup.Part1Index)
                {
                    ExaminedValue = Container.ContainerDescription.ConformanceIndicator
                };
            ValidationResults.Add(headerResult);
            IcddValidationLogger.Log(headerResult.ToString());
        }

        private void CheckIfLinksetsContained()
        {
            var nNotContained = 0;
            var files = Directory.GetFiles(Container.GetLinksetFolder());
            foreach (var doc in files)
            {
                var fileName = doc.Split('/').Last();


                var fileResult =
                    new IcddValidationResult(
                            "CONFORMITY: Is Link dataset  '" + fileName +
                            "' from the 'Payload triples' folder listed in the header file?",
                            IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Index)
                    { ExaminedValue = false };
                if (!Container.ContainerDescription.HasLinkset(fileName))
                {
                    nNotContained++;
                    IcddValidationLogger.Log(fileResult.ToString());
                    ValidationResults.Add(fileResult);
                }
                else
                {
                    fileResult.ExaminedValue = true;
                    IcddValidationLogger.Log(fileResult.ToString());
                }
            }
            if (nNotContained == 0)
            {
                var result = new IcddValidationResult(
                        "CONFORMITY: Are all Link datasets within the 'Payload triples' folder listed in the header file?",
                        IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Index)
                { ExaminedValue = true };
                ValidationResults.Add(result);
                IcddValidationLogger.Log(result.ToString());
            }
        }

        #endregion

        #region Documents

        private void CheckContainerContainsAllDocuments()
        {
            var nNotContained = 0;
            foreach (var doc in Container.ContainerDescription.ContainsDocument)
                if (doc.GetType() == typeof(CtInternalDocument) || doc.GetType().IsSubclassOf(typeof(CtInternalDocument)))
                {
                    if (doc.GetType() == typeof(CtFolderDocument))
                    {
                        var foldername = ((CtFolderDocument)doc).Foldername;
                        var folderResult =
                           new IcddValidationResult(
                                   "CONFORMITY: Is folder '" + foldername + "' stored in the 'Payload documents' folder?",
                                   IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Documents)
                           { ExaminedValue = false };
                        var folderPath = Path.Combine(Container.GetDocumentFolder(), foldername);
                        if (Directory.Exists(folderPath))
                        {
                            folderResult.ExaminedValue = true;
                        }
                        IcddValidationLogger.Log(folderResult.ToString());
                        ValidationResults.Add(folderResult);
                    }
                    else
                    {
                        var intDoc = (CtInternalDocument)doc;
                        var fileName = intDoc.FileName;
                        var fileResult =
                            new IcddValidationResult(
                                    "CONFORMITY: Is document '" + fileName + "' stored in the 'Payload documents' folder?",
                                    IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Documents)
                            { ExaminedValue = false };
                        var filePath = Container.GetDocumentFolder() + fileName;
                        if (File.Exists(filePath) || intDoc.Requested)
                        {
                            fileResult.ExaminedValue = true;
                            IcddValidationLogger.Log(fileResult.ToString());
                        }
                        else
                        {
                            nNotContained++;
                            IcddValidationLogger.Log(fileResult.ToString());
                            ValidationResults.Add(fileResult);
                        }
                    }

                }
            if (nNotContained == 0)
            {
                var result = new IcddValidationResult(
                        "CONFORMITY: Is each document listed in the container stored in the 'Payload documents' folder?",
                        IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Documents)
                { ExaminedValue = true };
                ValidationResults.Add(result);
                IcddValidationLogger.Log(result.ToString());
            }
        }

        #endregion

        #region LinkDatasets

        private void CheckLinksetRdf(CtLinkset linkset)
        {
            var headerResult =
                new IcddValidationResult("CONFORMITY: " + linkset.FileName + ": Complies with RDF/OWL specification?",
                    IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Linkset);
            var headerResult2 =
                new IcddValidationResult("CONFORMITY: " + linkset.FileName + ":  Is Rdf/XML-Abbreviated serialised?",
                    IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Linkset);
            try
            {
                var graph = new Graph();
                FileLoader.Load(graph, Container.GetLinksetFolder() + "/" + linkset.FileName);

                var triples = graph.Triples;
                var prefixes = graph.NamespaceMap;
                if (triples != null && triples.Count != 0 && prefixes != null && prefixes.Prefixes.Count() != 0)
                {
                    headerResult.ExaminedValue = true;
                    headerResult2.ExaminedValue = true;
                    ValidationResults.Add(headerResult);
                    ValidationResults.Add(headerResult2);
                    IcddValidationLogger.Log(headerResult.ToString());
                    IcddValidationLogger.Log(headerResult2.ToString());

                    return;
                }
                headerResult.ExaminedValue = false;
                headerResult2.ExaminedValue = false;
            }
            catch (Exception e)
            {
                Logger.Log("Could not Parse Index File. Exception: " + e, Logger.MsgType.Error, "CheckLinksetRdf()");
                headerResult.ExaminedValue = false;
                headerResult2.ExaminedValue = false;
            }
            ValidationResults.Add(headerResult);
            IcddValidationLogger.Log(headerResult.ToString());
            ValidationResults.Add(headerResult2);
            IcddValidationLogger.Log(headerResult2.ToString());
        }

        private void CheckIfLinksetExists(CtLinkset linkset)
        {
            var headerResult =
                new IcddValidationResult("CONFORMITY: " + linkset.FileName + ": Is stored in 'Payload triples' folder?",
                    IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Linkset);
            try
            {
                headerResult.ExaminedValue = File.Exists(Container.GetLinksetFolder() + "/" + linkset.FileName);
                ValidationResults.Add(headerResult);
                IcddValidationLogger.Log(headerResult.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                headerResult.ExaminedValue = false;
            }
            IcddValidationLogger.Log(headerResult.ToString());
        }

        private void CheckLinksetRdfOntologyReference(CtLinkset linkset)
        {
            var headerResult =
                new IcddValidationResult("CONFORMITY: " + linkset.FileName + ": Refers directly or indirectly to the Linkset.rdf ontology?",
                    IcddValidationResult.ValidBoolResult, ValidationGroup.Part1Linkset);
            try
            {
                var graph = linkset.LinksetGraph;
                if (graph == null)
                {
                    headerResult.ExaminedValue = false;
                    return;
                }

                var imports = RdfReader.GetImports(graph);

                if (imports.Any() && (imports.Contains(IcddNamespacesHelper.LINKSET_ONTOLOGY) || imports.Contains(IcddNamespacesHelper.LINKSET_URI_DRAFT)))
                {
                    headerResult.ExaminedValue = true;
                    ValidationResults.Add(headerResult);
                    IcddValidationLogger.Log(headerResult.ToString());
                    return;
                }
                headerResult.ExaminedValue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                headerResult.ExaminedValue = false;
            }
            ValidationResults.Add(headerResult);
            IcddValidationLogger.Log(headerResult.ToString());
        }

        #endregion

        #endregion

        #region Part2Methods

        //private void CheckConformanceIndicatorPart2()
        //{
        //    var headerResult =
        //        new IcddValidationResult("CONFORMITY: Contains the value 'part2' for the ct:conformanceIndicator property?",
        //            "part2", ValidationGroup.Part1Index)
        //        {
        //            ExaminedValue = Container.ContainerDescription.ConformanceIndicator
        //        };
        //    ValidationResults.Add(headerResult);
        //    IcddValidationLogger.Log(headerResult.ToString());
        //    Logger.Log("Conformance indicator for part2 not yet implemented.", Logger.MsgType.Error, "CheckConformanceIndicatorPart2()");
        //}

        #endregion

        #region PublicMethods

        internal int GetNumberOfValidationErrors()
        {
            return ValidationResults.Count(result => !result.ValidationResult());
        }

        public List<IcddValidationResult> GetResults()
        {
            return ValidationResults;
        }

        public bool IsValid()
        {
            return GetNumberOfValidationErrors() == 0;
        }

        public InformationContainer GetValidContainer()
        {
            return IsValid() ? Container : null;
        }

        #endregion
    }
}