using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace IIB.ICDD.Conversion.MsProjXml
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  MsProjConverter 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class CtoConverter : IConverter
    {
        private readonly CtInternalDocument _document;
        private readonly Graph _graph;
        private readonly string _workFolder = Path.Combine(FileHandler.GetWorkFolder(), "Converter", "cto",
                DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                DateTime.Now.ToShortTimeString().Replace(":", "-") + "-" + DateTime.Now.Second);

        private readonly string _inst;
        private const string _msproj = "http://schemas.microsoft.com/project";
        private const string _rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns";
        private const string _cto = "https://w3id.org/cto#";
        private const string _prov = "http://www.w3.org/ns/prov#";

        public CtoConverter(CtInternalDocument document)
        {
            _document = document;
            _graph = new Graph();
            _inst = IcddNamespacesHelper.BaseNamespaceFor(_document.Container, "cto", "inst");
        }

        public string ConvertToFile()
        {
            string outputFile = Path.ChangeExtension(Path.Combine(_workFolder, "converted_" + _document.Name), "ttl");
            if (!Directory.Exists(_workFolder))
            {
                Directory.CreateDirectory(_workFolder);
            }

            try
            {
                CompressingTurtleWriter writer = new(TurtleSyntax.W3C);
                writer.Save(ConvertToGraph(), outputFile);
                return outputFile;
            }
            catch (Exception e)
            {
                Logger.Log("Konnte RDF Datei nicht schreiben. Exception: " + e, Logger.MsgType.Error, "ConvertToGraph()");
                return "";
            }

        }

        public string ConvertToFile(string outputFile)
        {
            if (!Directory.Exists(_workFolder))
            {
                Directory.CreateDirectory(_workFolder);
            }

            try
            {
                CompressingTurtleWriter writer = new(TurtleSyntax.W3C);
                writer.Save(ConvertToGraph(), outputFile);
                return outputFile;
            }
            catch (Exception e)
            {
                Logger.Log("Konnte RDF Datei nicht schreiben. Exception: " + e, Logger.MsgType.Error, "ConvertToGraph()");
                return "";
            }

        }

        public Graph ConvertToGraph()
        {
            string file = _document.AbsoluteFilePath();
            XmlSerializer serializer = new(typeof(MsProjectXML.Project));

            if (!Directory.Exists(_workFolder))
            {
                Directory.CreateDirectory(_workFolder);
            }
            try
            {
                MsProjectXML.Project project;
                using (Stream reader = new FileStream(file, FileMode.Open))
                {
                    // Call the Deserialize method to restore the object's state.
                    project = (MsProjectXML.Project)serializer.Deserialize(reader);
                }

                _graph.NamespaceMap.AddNamespace("msproj", new Uri(_msproj + "#"));
                _graph.NamespaceMap.AddNamespace("prov", new Uri(_prov));
                _graph.NamespaceMap.AddNamespace("cto", new Uri(_cto));
                _graph.NamespaceMap.Import(IcddNamespacesHelper.RDF_NAMESPACE_MAP);
                _graph.BaseUri = new Uri(_inst);
                _graph.NamespaceMap.AddNamespace("inst", new Uri(_inst + "#"));

                if (project != null)
                {
                    AddProject(project);
                }

                // CompressingTurtleWriter writer = new CompressingTurtleWriter(TurtleSyntax.W3C);
                //var outputFile = Path.Combine(_workFolder, "msprojxml.ttl");
                //writer.Save(_graph, outputFile);
            }
            catch (Exception e)
            {
                Logger.Log("Konnte RDF Datei nicht schreiben. Exception: " + e, Logger.MsgType.Error, "ConvertToGraph()");
            }
            return _graph;
        }

        private void AddProject(MsProjectXML.Project project)
        {
            IUriNode projectNode = project.Name != null ? new IcddResourceWrapper(_inst, RemoveBadChars(project.Name), _graph).AsNode() : new IcddResourceWrapper(_inst, RemoveBadChars("ConstructionProject"), _graph).AsNode();
            IUriNode typePredicateNode = new IcddResourceWrapper(_rdf, "type", _graph).AsNode();
            IUriNode typeObjectNode = new IcddResourceWrapper(_cto, "TaskContext", _graph).AsNode();
            IUriNode typeObjectNode2 = new IcddResourceWrapper(_msproj, "Project", _graph).AsNode();
            _graph.Assert(new Triple(projectNode, typePredicateNode, typeObjectNode));
            _graph.Assert(new Triple(projectNode, typePredicateNode, typeObjectNode2));


            foreach (MsProjectXML.ProjectTask task in project.Tasks)
            {
                try
                {
                    IUriNode predNode = new IcddResourceWrapper(_cto, "hasTaskContext", _graph).AsNode();
                    IUriNode taskNode = new IcddResourceWrapper(_inst, GetUriFriendlyName(task), _graph).AsNode();
                    IUriNode taskTypeObjectNode = new IcddResourceWrapper(_cto, "Task", _graph).AsNode();
                    IUriNode taskTypeObjectNode2 = new IcddResourceWrapper(_msproj, "Task", _graph).AsNode();
                    _graph.Assert(new Triple(taskNode, typePredicateNode, taskTypeObjectNode));
                    _graph.Assert(new Triple(taskNode, typePredicateNode, taskTypeObjectNode2));
                    _graph.Assert(new Triple(taskNode, predNode, projectNode));

                 
                    if (task.Name != null)
                    {
                        IUriNode taskDescriptionNode = new IcddResourceWrapper(_cto, "hasSimpleTaskMethodDescription", _graph).AsNode();
                        ILiteralNode descriptionNode = _graph.CreateLiteralNode(task.Name);
                        _graph.Assert(new Triple(taskNode, taskDescriptionNode, descriptionNode));
                    }

                    if (task.UID != null)
                    {
                        IUriNode predicateNode = new IcddResourceWrapper(_msproj, "UID", _graph).AsNode();
                        ILiteralNode objectNode = _graph.CreateLiteralNode(task.UID);
                        _graph.Assert(new Triple(taskNode, predicateNode, objectNode));
                    }

                    {
                        IUriNode predicateNode = new IcddResourceWrapper(_prov, "startedAtTime", _graph).AsNode();
                        ILiteralNode objectNode = task.Start.ToLiteral(_graph);
                        _graph.Assert(new Triple(taskNode, predicateNode, objectNode));
                    }

                    {
                        IUriNode predicateNode = new IcddResourceWrapper(_prov, "endedAtTime", _graph).AsNode();
                        ILiteralNode objectNode = task.Finish.ToLiteral(_graph);
                        _graph.Assert(new Triple(taskNode, predicateNode, objectNode));
                    }

                    if (task.PredecessorLink == null) continue;

                    foreach (MsProjectXML.ProjectTaskPredecessorLink predecessorLink in task.PredecessorLink)
                    {
                        if (string.IsNullOrEmpty(predecessorLink.PredecessorUID)) continue;

                        MsProjectXML.ProjectTask predecessor = project.Tasks.ToList().Find(tsk => tsk.UID == predecessorLink.PredecessorUID);
                        if (predecessor == null) continue;

                        IUriNode predecessorNode = new IcddResourceWrapper(_inst, GetUriFriendlyName(predecessor), _graph).AsNode();
                        IUriNode afterFinishedTaskNode = new IcddResourceWrapper(_cto, "afterFinishedTask", _graph).AsNode();
                        _graph.Assert(new Triple(taskNode, afterFinishedTaskNode, predecessorNode));
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("Error when converting Task " + task.Name + "_" + task.UID + ": " + e, Logger.MsgType.Error, "CtoConverter.AddProject");
                }
            }
        }
        private static string RemoveBadChars(string originalString)
        {
            return originalString.Replace(" ", string.Empty).ToUpper()
                .Replace("Ä", "AE")
                .Replace("Ö", "OE")
                .Replace("Ü", "UE")
                .Replace("ß", "SS"); 
        }

        private static string GetUriFriendlyName(MsProjectXML.ProjectTask task)
        {
            return task.Name == null ? "Task_" + task.UID  : "Task_" + task.UID + "_" + RemoveBadChars(task.Name);
        }
    }
}
