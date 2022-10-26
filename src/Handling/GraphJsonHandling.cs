using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IIB.ICDD.Model;
using VDS.RDF;


namespace IIB.ICDD.Handling
{
    public class GraphJsonHandling
    {
        [JsonInclude]
        [JsonPropertyName("nodes")]
        public List<Node> Nodes { get; set; } = new();

        [JsonInclude]
        [JsonPropertyName("edges")]
        public List<Edge> Edges { get; set; } = new();

        public GraphJsonHandling(IcddBaseElement node)
        {
            var graph = node.AsSubgraph();
            graph.Nodes.ToList().ForEach(nd => Nodes.Add(new Node(nd,node)));

            graph.Triples.ToList().ForEach(trip=>Edges.Add(new Edge(trip, Nodes, node)));
            

        }
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    [DebuggerDisplay("{Label}")]
    public class Node
    {
        [JsonInclude]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonIgnore]
        public INode NodeReference { get; set; }

        [JsonInclude]
        [JsonPropertyName("label")]
        public string Label { get; set; }

        public Node(INode nodeReference, IcddBaseElement containerReference)
        {
            NodeReference = nodeReference;
            Id = ContainerHandler.Encode(Guid.NewGuid());
            var namespaceMapper = new NamespaceMapper();
            namespaceMapper.Import(containerReference.Container.IndexGraph.NamespaceMap);
            namespaceMapper.Import(containerReference.Graph.NamespaceMap);
            namespaceMapper.AddNamespace("cinst", containerReference.Container.IndexGraph.BaseUri);
            namespaceMapper.AddNamespace("linst", containerReference.Graph.BaseUri);
            Label = ContainerHandler.GetNodeFormatter(namespaceMapper).Format(nodeReference);
        }
    }

    [DebuggerDisplay("{Label}")]
    public class Edge
    {
        [JsonInclude]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonInclude]
        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonInclude]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonInclude]
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonIgnore]
        public Triple TripleReference { get; set; }

        public Edge(Triple triple, List<Node> nodeReferences, IcddBaseElement containerReference)
        {
            var namespaceMapper = new NamespaceMapper();
            namespaceMapper.Import(containerReference.Container.IndexGraph.NamespaceMap);
            namespaceMapper.Import(containerReference.Graph.NamespaceMap);
            
            TripleReference = triple;
            Id = ContainerHandler.Encode(Guid.NewGuid());
            From = nodeReferences.Find(subj => subj.NodeReference.Equals(triple.Subject))?.Id;
            To = nodeReferences.Find(obj => obj.NodeReference.Equals(triple.Object))?.Id;
            Label = ContainerHandler.GetNodeFormatter(namespaceMapper).Format(triple.Predicate);
        }
    }
}
