using System;
using System.IO;
using System.Linq;
using IIB.ICDD.Conversion.Ifc;
using IIB.ICDD.Conversion.MsProjXml;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container.Document;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace IIB.ICDD.Conversion.Container
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddConverter 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddConverter
    {
        private InformationContainer _container;
        private readonly string _workFolder =
            Path.Combine(FileHandler.GetWorkFolder(),
                "Converter", "ICDD",
                DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                DateTime.Now.ToShortTimeString().Replace(":", "-") + "-" + DateTime.Now.Second);

        public IcddConverter(InformationContainer container)
        {
            _container = container;
        }

        public Graph ConvertToGraph(bool convertData = false, bool convertLinkedData = true)
        {
            var dataGraph = new Graph();
            dataGraph.Merge(_container.IndexGraph, true);
            foreach (var ls in _container.ContainerDescription.ContainsLinkset)
            {
                dataGraph.Merge(ls.LinksetGraph, true);
            }

            if (convertData)
            {
                foreach (var document in _container.ContainerDescription.ContainsDocument)
                {
                    if (document.GetType() == typeof(CtInternalDocument))
                    {
                        var internalDocument = document as CtInternalDocument;
                        if (document.FileType.Contains("ifc") || document.FileType.Contains("IFC"))
                        {
                            dataGraph.Merge(new IfcConverter(internalDocument).ConvertToGraph(), true);
                        }
                        if (document.FileType.Contains("xml") || document.FileType.Contains("XML"))
                        {
                            dataGraph.Merge(new MsProjConverter(internalDocument).ConvertToGraph(), true);
                        }
                    }
                }
            }

            if (convertLinkedData)
            {
                foreach (var document in _container.ContainerDescription.ContainsDocument)
                {
                    if (document.GetType() == typeof(CtInternalDocument))
                    {
                        if (document.FileType.Contains("ttl") || document.FileType.Contains("TTL"))
                        {
                            try
                            {
                                Graph g = new();
                                FileLoader.Load(g, Path.Combine(_container.GetDocumentFolder(), document.Name));
                                dataGraph.Merge(g, true);
                            }
                            catch (Exception e)
                            {
                                Logger.Log(e.Message, Logger.MsgType.Error, "IcddConverter.ConvertToGraph");
                            }
                        }
                    }
                }


                // DirectoryInfo payloadTriplesFolder = new DirectoryInfo(_container.GetLinksetFolder());
                foreach (var payloadTriple in _container.PayloadTriples)
                {
                    try
                    {
                        Graph g = payloadTriple.GetGraph();

                        if (dataGraph.ContainsTriple(g.Triples.First()))
                            continue;

                        dataGraph.Merge(g, true);
                    }
                    catch (Exception e)
                    {
                        Logger.Log(e.Message, Logger.MsgType.Error, "IcddConverter.ConvertToGraph");
                    }


                }
            }

            return dataGraph;
        }

        public TripleStore ConvertToTripleStore(bool convertData = false, bool convertLinkedData = true)
        {
            var dataGraph = new TripleStore();
            dataGraph.Add(_container.IndexGraph, true);
            foreach (var ls in _container.ContainerDescription.ContainsLinkset)
            {
                dataGraph.Add(ls.LinksetGraph, true);
            }

            if (convertData)
            {
                foreach (var document in _container.ContainerDescription.ContainsDocument)
                {
                    if (document.GetType() == typeof(CtInternalDocument))
                    {
                        var internalDocument = document as CtInternalDocument;
                        if (document.FileType.Contains("ifc") || document.FileType.Contains("IFC"))
                        {
                            dataGraph.Add(new IfcConverter(internalDocument).ConvertToGraph(), true);
                        }
                        if (document.FileType.Contains("xml") || document.FileType.Contains("XML"))
                        {
                            dataGraph.Add(new MsProjConverter(internalDocument).ConvertToGraph(), true);
                        }
                    }
                }
            }

            if (convertLinkedData)
            {
                foreach (var document in _container.ContainerDescription.ContainsDocument)
                {
                    if (document.GetType() == typeof(CtInternalDocument))
                    {
                        if (document.FileType.Contains("ttl") || document.FileType.Contains("TTL"))
                        {
                            try
                            {
                                Graph g = new();
                                FileLoader.Load(g, Path.Combine(_container.GetDocumentFolder(), document.Name));
                                dataGraph.Add(g, false);
                            }
                            catch (Exception e)
                            {
                                Logger.Log(e.Message, Logger.MsgType.Error, "IcddConverter.ConvertToTripleStore");
                            }
                        }
                    }
                }

                DirectoryInfo payloadTriplesFolder = new(_container.GetLinksetFolder());
                foreach (FileInfo document in payloadTriplesFolder.GetFiles())
                {
                    try
                    {
                        Graph g = new();
                        FileLoader.Load(g, document.FullName);

                        if (dataGraph.Contains(g.Triples.First()))
                            continue;

                        dataGraph.Add(g, false);
                    }
                    catch (Exception e)
                    {
                        Logger.Log(e.Message, Logger.MsgType.Error, "IcddConverter.ConvertToTripleStore");
                    }


                }
            }

            return dataGraph;
        }
        public string ConvertToFile(string outputFile = null)
        {
            if (outputFile == null)
                outputFile = Path.ChangeExtension(Path.Combine(_workFolder, "converted_" + _container.ContainerName), "ttl");
            if (!Directory.Exists(_workFolder))
            {
                Directory.CreateDirectory(_workFolder);
            }
            Graph dataGraph = ConvertToGraph();           
            CompressingTurtleWriter writer = new(TurtleSyntax.W3C);
            writer.Save(dataGraph, outputFile);
            return outputFile;
        }
    }
}
