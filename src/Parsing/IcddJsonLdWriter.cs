using System.Data;
using System.Text;
using AngleSharp.Css;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Linkset.Link;
using IIB.ICDD.Parsing.Vocabulary;
using Newtonsoft.Json.Linq;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;
using JsonLdProcessor = VDS.RDF.JsonLd.JsonLdProcessor;
using StringWriter = System.IO.StringWriter;


namespace IIB.ICDD.Parsing
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddJsonLdWriter 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddJsonLdWriter
    {
        public IcddBaseElement Node;
        public TripleStore Store;

        public IcddJsonLdWriter(IcddBaseElement node)
        {
            Node = node;
            Store = new TripleStore();
            if (node.IsSameOrSubclass(typeof(LsLink)))
            {
                LsLink link = node as LsLink;
                Store.Add(link?.AsSubgraph());
            }
            else
            {
                var triples = Node.Graph.GetTriplesWithSubject(Node.AsNode());
                Graph g = new();
                g.Assert(triples);
                Store.Add(g);
            }
        }

        public JObject Write()
        {

            JsonLdWriter writer = new();
            StringBuilder build = new();
            StringWriter write = new(build);
            writer.Save(Store, write);

            switch (Node)
            {
                case CtLinkset _:
                    return JsonLdProcessor.Compact(writer.SerializeStore(Store), JToken.Parse(IcddContextHelper.CtLinkset), new JsonLdProcessorOptions());
                case CtPerson _:
                    return JsonLdProcessor.Compact(writer.SerializeStore(Store), JToken.Parse(IcddContextHelper.CtPerson), new JsonLdProcessorOptions());
                case CtOrganisation _:
                    return JsonLdProcessor.Compact(writer.SerializeStore(Store), JToken.Parse(IcddContextHelper.CtOrganisation), new JsonLdProcessorOptions());
                default:
                    return JsonLdProcessor.Compact(writer.SerializeStore(Store), JToken.Parse(IcddContextHelper.FullContext), new JsonLdProcessorOptions());

            }

        }
    }
}
