using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;
using VDS.RDF.Writing.Formatting;

namespace IIB.ICDD.Handling
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  ContainerHandler 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal static class ContainerHandler
    {
        internal static List<Uri> OrganizeImports(Graph graph, string tempPath)
        {
            List<Uri> imports = RdfReader.GetImports(graph);
            if (!imports.Any())
            {
                imports.Add(IcddNamespacesHelper.CONTAINER_ONTOLOGY);
                imports.Add(IcddNamespacesHelper.LINKSET_ONTOLOGY);
                imports.Add(IcddNamespacesHelper.EXT_LINKSET_ONTOLOGY);
                DownloadImports(tempPath);
            }
            graph.NamespaceMap.Import(IcddNamespacesHelper.ICDD_NAMESPACE_MAP);
            return imports;
        }
        internal static List<Uri> CreateImports(InformationContainer container, string tempPath)
        {
            container.IndexGraph.NamespaceMap.Import(IcddNamespacesHelper.ICDD_NAMESPACE_MAP);
            RdfWriter.InitializeImports(container.IndexGraph);
            DownloadImports(tempPath);
            return RdfReader.GetImports(container.IndexGraph);
        }

        internal static bool SetBaseNamespace(InformationContainer container, string baseNamespace = "")
        {
            string baseNs = string.IsNullOrEmpty(baseNamespace) ? IcddNamespacesHelper.BaseNamespaceFor(container, "index", "") : baseNamespace;
            container.IndexGraph.NamespaceMap.Import(IcddNamespacesHelper.ICDD_NAMESPACE_MAP);
            container.IndexGraph.NamespaceMap.AddNamespace("", new Uri(baseNs + "#"));
            container.IndexGraph.BaseUri = new Uri(baseNs);

            return container.IndexGraph.BaseUri != null;
        }

        internal static void OrganizeDuplicateNamespaces(Graph graph)
        {
            var namespaceMap = new NamespaceMapper();
            foreach (var pref in graph.NamespaceMap.Prefixes)
            {
                if (!namespaceMap.HasNamespace(graph.NamespaceMap.GetNamespaceUri(pref)))
                {
                    namespaceMap.AddNamespace(pref, graph.NamespaceMap.GetNamespaceUri(pref));
                }
            }
            graph.NamespaceMap.Clear();
            graph.NamespaceMap.Import(namespaceMap);
        }
        private static void DownloadImports(string path)
        {
            try
            {
                using (WebClient webClient = new())
                {
                    webClient.DownloadFile(IcddNamespacesHelper.CONTAINER_ONTOLOGY.AbsoluteUri,
                        Path.Combine(path, "Ontology Resources/Container.rdf"));
                    webClient.DownloadFile(IcddNamespacesHelper.LINKSET_ONTOLOGY.AbsoluteUri,
                        Path.Combine(path, "Ontology Resources/Linkset.rdf"));
                    webClient.DownloadFile(IcddNamespacesHelper.EXT_LINKSET_ONTOLOGY.AbsoluteUri,
                        Path.Combine(path, "Ontology Resources/ExtendedLinkset.rdf"));
                    webClient.DownloadFile(IcddNamespacesHelper.EXT_DOCUMENT_ONTOLOGY.AbsoluteUri,
                        Path.Combine(path, "Ontology Resources/ExtendedDocument.rdf"));
                }
            }
            catch
            {
                Logger.Log("Could not download referenced Resources.", Logger.MsgType.Warning, "CreateContainer");
            }
        }
        internal static List<IcddOntology> OrganizeOntologies(List<Uri> imports, string ontologyFolder)
        {
            List<IcddOntology> ontologies = new List<IcddOntology>();
            foreach (Uri ontologyFile in imports)
            {
                string path = Path.Combine(ontologyFolder, Path.GetFileName(ontologyFile.AbsoluteUri));
                IcddOntology udo = new(path);


                bool isIncluded = false;
                bool isContainerOntology = udo.GetFileName().Equals("Container.rdf") ||
                                           udo.GetFileName().Equals("Linkset.rdf") ||
                                           udo.GetFileName().Equals("ExtendedLinkset.rdf") ||
                                           udo.GetFileName().Equals("DynamicSemantics.rdf") ||
                                           udo.GetFileName().Equals("ExtendedDocument.rdf") ||
                                           udo.GetFileName().Equals("index.rdf");
                bool fileExists = File.Exists(path);
                if (fileExists && !isContainerOntology)
                {
                    foreach (IcddOntology onto in ontologies)
                    {
                        if (!onto.GetFileName().Equals(udo.GetFileName())) continue;
                        isIncluded = true;
                        break;
                    }
                }
                if (!isContainerOntology && !isIncluded && fileExists)
                    ontologies.Add(udo);
            }

            DirectoryInfo di = new(ontologyFolder);

            foreach (var file in di.GetFiles())
            {
                IcddOntology udo = new(file.FullName);

                bool isContainerOntology = udo.GetFileName().Contains("Container.rdf") ||
                                           udo.GetFileName().Contains("Linkset.rdf") ||
                                           udo.GetFileName().Contains("ExtendedLinkset.rdf") ||
                                           udo.GetFileName().Contains("DynamicSemantics.rdf") ||
                                           udo.GetFileName().Contains("index.rdf");
                if (!isContainerOntology)
                    ontologies.Add(udo);
            }
            return ontologies;
        }
        internal static string Index(string path)
        {
            if (File.Exists(Path.Combine(path, "index.rdf")))
            {
                return Path.Combine(path, "index.rdf");
            }
            else
            {
                Logger.Log("Could not find index.rdf file", Logger.MsgType.Error, "IcddContainer(string guid, string filename)");
            }

            return "";
        }

        public static string Encode(Guid guid)
        {
            string enc = Convert.ToBase64String(guid.ToByteArray());
            enc = enc.Replace("/", "_");
            enc = enc.Replace("+", "-");
            return enc.Substring(0, 22);
        }

        public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase)
                   || potentialDescendant == potentialBase;
        }

        public static bool IsSameClass(Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant == potentialBase;
        }

        public static BaseFormatter GetNodeFormatter(INamespaceMapper nsmap = null)
        {
            nsmap ??= IcddNamespacesHelper.ICDD_NAMESPACE_MAP;
            return new TurtleFormatter(nsmap);
        }

        public static INodeFormatter GetPlainNodeFormatter(INamespaceMapper nsmap = null)
        {
            nsmap ??= IcddNamespacesHelper.ICDD_NAMESPACE_MAP;
            return new PlainNodeFormatter(nsmap);
        }
    }

    public class PlainNodeFormatter : QNameFormatter
    {
        protected List<string[]> _longLitMustEscape = new()
        {
            new string[] { @"\", @"\\" },
            new string[] { "\"", "\\\"" },
        };

        /// <summary>
        /// Set of characters that must be escaped for Literals.
        /// </summary>
        protected List<string[]> _litMustEscape = new()
        {
            new string[] { @"\", @"\\" },
            new string[] { "\"", "\\\"" },
            new string[] { "\n", @"\n" },
            new string[] { "\r", @"\r" },
            new string[] { "\t", @"\t" },
        };

        public PlainNodeFormatter(INamespaceMapper nsmap) : base("Plain", new QNameOutputMapper(nsmap)) { }

        protected override string FormatLiteralNode(ILiteralNode l, TripleSegment? segment)
        {
            var output = new StringBuilder();
            bool longlit, plainlit;

            longlit = TurtleSpecsHelper.IsLongLiteral(l.Value);
            plainlit = TurtleSpecsHelper.IsValidPlainLiteral(l.Value, l.DataType, TurtleSyntax.Original);

            if (plainlit)
            {
                if (TurtleSpecsHelper.IsValidDecimal(l.Value) && l.Value.EndsWith("."))
                {
                    // Ensure we strip the trailing dot of any xsd:decimal and add a datatype definition
                    output.Append(l.Value.Substring(0, l.Value.Length - 1));
                }
                else
                {
                    // Otherwise just write out the value
                    output.Append(l.Value);
                }
                // For integers ensure we insert a space after the literal to ensure it can't ever be confused with a decimal

            }
            else
            {

                var value = l.Value;
                value = longlit ? Escape(value, _longLitMustEscape) : Escape(value, _litMustEscape);
                output.Append(value);

            }

            return output.ToString();
        }

        public override string FormatNamespace(string prefix, Uri namespaceUri)
        {

            return "@prefix " + prefix + ": <" + FormatUri(namespaceUri) + "> .";
        }

        protected override string FormatUriNode(IUriNode u, TripleSegment? segment)
        {
            return u.Uri.AbsoluteUri;
        }
    }
}
