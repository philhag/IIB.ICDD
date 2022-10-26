using System.Collections.Generic;
using System.IO;
using System.Linq;
using IIB.ICDD.Conversion.Container;
using IIB.ICDD.Model;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Inference;

namespace IIB.ICDD.Reasoning
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddShaclReasoner 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal class IcddRdfsReasoner
    {
        public IGraph DataGraph = new Graph();
        public IGraph SchemaGraph = new Graph();
        public IInferenceEngine Reasoner;

        private IcddRdfsReasoner(IGraph data, IGraph schema)
        {
            DataGraph.Merge(data, true);
            SchemaGraph.Merge(schema, true);
            Reasoner = new StaticRdfsReasoner();
            Reasoner.Initialise(SchemaGraph);
        }

        public IGraph ApplyInference()
        {
            Reasoner.Apply(DataGraph);
            return DataGraph;
        }

        public static IcddRdfsReasoner Create(IGraph data, IGraph schema)
        {
            return new IcddRdfsReasoner(data, schema);
        }

        public static IcddRdfsReasoner Create(string dataFile, string schemaFile)
        {
            Graph data = new();
            FileLoader.Load(data, dataFile);
            Graph schema = new();
            FileLoader.Load(schema, schemaFile);
            return Create(data, schema);
        }

        public static IcddRdfsReasoner Create(List<IGraph> dataGraphs, List<IGraph> schemaGraphs)
        {
            Graph data = new();
            dataGraphs.ForEach(graph => data.Merge(graph, true));
            Graph schema = new();
            schemaGraphs.ForEach(graph => schema.Merge(graph, true));
            return Create(data, schema);
        }

        public static IcddRdfsReasoner Create(InformationContainer container, bool convertData = false)
        {
            Graph data = new();
            Graph schema = new();

            DirectoryInfo directory = new(container.GetOntologyFolder());

            directory.GetFiles()?.ToList().ForEach(file => schema.Merge(new IcddOntology(file.FullName).GetOntology().Graph, true));
            IcddConverter converter = new(container);
            data.Merge(converter.ConvertToGraph(convertData), true);

            return Create(data, schema);
        }

    }
}
