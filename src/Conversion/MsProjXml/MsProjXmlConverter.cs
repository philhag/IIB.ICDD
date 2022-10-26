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
    public class MsProjConverter : IConverter
    {
        private CtInternalDocument _document;
        private Graph _graph;
        private readonly string _workFolder = Path.Combine(FileHandler.GetWorkFolder(), "Converter", "MsProjXml",
                DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                DateTime.Now.ToShortTimeString().Replace(":", "-") + "-" + DateTime.Now.Second);

        private string _inst;
        private const string _msproj = "http://schemas.microsoft.com/project";
        private const string _rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns";

        public MsProjConverter(CtInternalDocument document)
        {
            _document = document;
            _graph = new Graph();
            _inst = IcddNamespacesHelper.BaseNamespaceFor(_document.Container, "msprojxml", "instances");
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
                _graph.NamespaceMap.Import(IcddNamespacesHelper.RDF_NAMESPACE_MAP);
                _graph.BaseUri = new Uri(_inst);
                _graph.NamespaceMap.AddNamespace("sched", new Uri(_inst + "#"));

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
            int nPredecessor = 0;
            var projectNode = new IcddResourceWrapper(_inst, project.Name, _graph).AsNode();
            var typePredicateNode = new IcddResourceWrapper(_rdf, "type", _graph).AsNode();
            var typeObjectNode = new IcddResourceWrapper(_msproj, "Project", _graph).AsNode();
            _graph.Assert(new Triple(projectNode, typePredicateNode, typeObjectNode));

            foreach (var pInfo in project.GetType().GetProperties())
            {
                var predNode = new IcddResourceWrapper(_msproj, pInfo.Name, _graph).AsNode();
                if (pInfo.Name != "Tasks")
                {
                    var val = pInfo.GetValue(project, BindingFlags.GetProperty, null, null, null) ?? "";
                    var litNode = _graph.CreateLiteralNode(val.ToString());
                    _graph.Assert(new Triple(projectNode, predNode, litNode));
                }
            }

            foreach (var task in project.Tasks)
            {
                var predNode = new IcddResourceWrapper(_msproj, "hasTask", _graph).AsNode();
                var taskNode = new IcddResourceWrapper(_inst, "Task_" + task.ID + "_" + task.Name, _graph).AsNode();
                var taskTypeObjectNode = new IcddResourceWrapper(_msproj, "Task", _graph).AsNode();
                _graph.Assert(new Triple(taskNode, typePredicateNode, taskTypeObjectNode));
                _graph.Assert(new Triple(projectNode, predNode, taskNode));

                foreach (var pInfo in task.GetType().GetProperties())
                {
                    var dynamicPredNode = new IcddResourceWrapper(_msproj, pInfo.Name, _graph).AsNode();
                    if (pInfo.Name != "PredecessorLink" && pInfo.Name != "ExtendedAttribute")
                    {
                        var val = pInfo.GetValue(task, BindingFlags.GetProperty, null, null, null) ?? "";
                        var litNode = _graph.CreateLiteralNode(val.ToString());
                        _graph.Assert(new Triple(taskNode, dynamicPredNode, litNode));
                    }
                }

                if (task.PredecessorLink == null) continue;

                foreach (var predecessorLink in task.PredecessorLink)
                {
                    if (string.IsNullOrEmpty(predecessorLink.PredecessorUID)) continue;

                    var predecessorPredicateNode = new IcddResourceWrapper(_msproj, "hasPredecessorLink", _graph).AsNode();
                    var subjPredecessorNode = new IcddResourceWrapper(_inst, "PredecessorLink_" + nPredecessor, _graph).AsNode();
                    var predecessorTypeObjectNode = new IcddResourceWrapper(_msproj, "PredecessorLink", _graph).AsNode();
                    _graph.Assert(new Triple(subjPredecessorNode, typePredicateNode, predecessorTypeObjectNode));
                    _graph.Assert(new Triple(taskNode, predecessorPredicateNode, subjPredecessorNode));

                    var predecessorTask = project.Tasks.ToList().Find(t => t.UID == predecessorLink.PredecessorUID);
                    var predHasPredecessorNode = new IcddResourceWrapper(_msproj, "hasPredecessor", _graph).AsNode();
                    var objPredecessorNode = _graph.CreateUriNode(new Uri(_inst + "#Task_" + task.ID + "_" + task.Name));
                    _graph.Assert(new Triple(subjPredecessorNode, predHasPredecessorNode, objPredecessorNode));
                    nPredecessor++;
                }
            }
        }
    }
}
