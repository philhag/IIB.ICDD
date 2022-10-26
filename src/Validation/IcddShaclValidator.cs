using System;
using System.Collections.Generic;
using System.IO;
using IIB.ICDD.Conversion.Container;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using IIB.ICDD.Reasoning;
using VDS.RDF;
using VDS.RDF.Shacl;
using VDS.RDF.Shacl.Validation;
using VDS.RDF.Writing;
using StringWriter = VDS.RDF.Writing.StringWriter;

namespace IIB.ICDD.Validation
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddShaclValidator 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// 
    public class IcddShaclValidator
    {
        protected ShapesGraph ShapesGraph;
        protected Graph DataGraph;

        #region Constructors

        public IcddShaclValidator(ShapesGraph shapes, Graph data)
        {
            DataGraph = data;
            ShapesGraph = shapes;
        }

        public IcddShaclValidator(Graph data)
        {
            DataGraph = data;
            ShapesGraph = TurtleReader.ReadShapes(IcddNamespacesHelper.CONTAINER_SHACL);
        }

        public IcddShaclValidator(InformationContainer container, bool convertData = false, bool applyInference = false)
        {
            DataGraph = new Graph();

            if (applyInference)
            {
                DataGraph.Merge(IcddRdfsReasoner.Create(container, convertData).ApplyInference(), true);
            }
            else
            {
                DataGraph.Merge(new IcddConverter(container).ConvertToGraph(convertData), true);
            }

            ShapesGraph = TurtleReader.ReadShapes(IcddNamespacesHelper.CONTAINER_SHACL);
        }

        public IcddShaclValidator(InformationContainer container, string shapesPath, bool convertData = false, bool applyInference = false)
        {
            DataGraph = new Graph();
            if (applyInference)
            {
                DataGraph.Merge(IcddRdfsReasoner.Create(container, convertData).ApplyInference(), true);
            }
            else
            {
                DataGraph.Merge(new IcddConverter(container).ConvertToGraph(convertData), true);
            }

            ShapesGraph = shapesPath == null || File.Exists(shapesPath) ?
                TurtleReader.ReadShapes(IcddNamespacesHelper.CONTAINER_SHACL) :
                TurtleReader.ReadShapes(shapesPath);
        }

        public IcddShaclValidator(InformationContainer container, ShapesGraph shapes, bool convertData = false, bool applyInference = false)
        {
            DataGraph = new Graph();
            if (applyInference)
            {
                DataGraph.Merge(IcddRdfsReasoner.Create(container, convertData).ApplyInference(), true);
            }
            else
            {
                DataGraph.Merge(new IcddConverter(container).ConvertToGraph(convertData), true);
            }

            ShapesGraph = shapes;
        }

        #endregion

        #region Methods
        public Report ValidateShaclResult()
        {
            try
            {
                return ShapesGraph.Validate(DataGraph);
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, Logger.MsgType.Error, "ValidateShaclResult");
                throw new IcddException("Error validating Shacl Shape.", e);
            }

        }

        public List<ShaclValidationResult> Results()
        {
            Report report = ValidateShaclResult();
            IcddValidationLogger.Initialize(System.IO.Path.Combine(FileHandler.GetLoggingFolder(), Guid.NewGuid() + "_ShaclValidation.log"));
            IcddValidationLogger.Log("SHACL Validation Protocol:");
            CompressingTurtleWriter rdfxmlwriter = new();
            Graph graph = (Graph)report.Normalised;
            string data = StringWriter.Write(graph, rdfxmlwriter);
            IcddValidationLogger.Log("RAW-RESULT: " + data);
            IcddValidationLogger.Save();
            return ShaclValidationResult.FromGraph(graph);
        }

        public bool Conforms()
        {
            return ShapesGraph.Conforms(DataGraph);
        }
        #endregion
    }
}
