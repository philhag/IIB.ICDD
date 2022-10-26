using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Container.ExtendedDocument;
using IIB.ICDD.Model.ExtendedLinkset;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Model.Linkset.Identifier;
using IIB.ICDD.Model.Linkset.Link;
using IIB.ICDD.Parsing.Vocabulary;
using TCode.r2rml4net.Extensions;
using VDS.RDF;
using VDS.RDF.Nodes;

namespace IIB.ICDD.Parsing
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  RdfReader 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal static class RdfReader
    {
        #region Namespaces

        /// <summary>
        /// Gets the namespace URI from an RDF Graph.
        /// </summary>
        /// <param name="graph">The RDF graph.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>Returns a <c>string</c> containing the respective Namespace URI.</returns>
        internal static string GetNamespaceUri(Graph graph, string prefix)
        {
            string result = "";
            try
            {
                var ns = graph.NamespaceMap.GetNamespaceUri(prefix).AbsoluteUri;
                result = ns.Split('#').FirstOrDefault();
            }
            catch (Exception e)
            {
                Logger.Log("Could not find namespace " + prefix + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.GetNamespaceUri(...)");
            }
            return result;
        }

        #endregion

        #region General
        internal static List<INode> GetInstances(Graph graph, IcddResourceWrapper subj, Uri property)
        {
            var result = new List<INode>();

            try
            {
                var subjectNode = subj.AsNode();
                var predicateNode = graph.CreateUriNode(property);
                var descNodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode);
                result.AddRange(descNodes.Select(node => node.Object));
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Instances for  " + property.Fragment + " - Exception: " + e, Logger.MsgType.Error,
                    "RdfReader.GetInstanceIDs(...)");
            }
            return result;
        }

        internal static List<INode> GetInstances(Graph graph, Uri type)
        {
            var result = new List<INode>();
            try
            {
                var predicateNode = graph.CreateUriNode(IcddNamespacesHelper.RDF_TYPE_URI);
                var objectNode = graph.CreateUriNode(type);
                var descNodes = graph.GetTriplesWithPredicateObject(predicateNode, objectNode);
                result.AddRange(descNodes.Select(node => node.Subject));
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Instances for  " + type.Fragment + " - Exception: " + e, Logger.MsgType.Error,
                    "RdfReader.GetInstances(...)");
            }
            return result;
        }

        internal static string GetInstanceType(Graph graph, INode resource)
        {
            if (resource == null)
                return string.Empty;

            try
            {
                var instances = graph.GetTriplesWithSubjectPredicate(resource, graph.CreateUriNode(IcddNamespacesHelper.RDF_TYPE_URI)).ToList();
                return instances.Any() ? instances.First().Object.GetNodeUri().Fragment.TrimStart('#') : string.Empty;
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Type for  " + resource + " - Exception: " + e, Logger.MsgType.Error,
                    "RdfReader.GetInstanceType(...)");
                return string.Empty;
            }
        }

        internal static List<Uri> GetImports(Graph graph)
        {
            var result = new List<Uri>();
            try
            {
                var owl = GetNamespaceUri(graph, "owl");
                var implementsNode = graph.CreateUriNode(new Uri(owl + "#imports"));
                var triples = graph.GetTriplesWithPredicate(implementsNode);
                foreach (var triple in triples)
                {
                    if (triple.Object.NodeType == NodeType.Uri)
                    {
                        var uri = ((UriNode)triple.Object).Uri;
                        if (!uri.AbsoluteUri.EndsWith(".rdf"))
                        {
                            uri = new Uri(uri.AbsoluteUri + ".rdf");
                        }
                        result.Add(uri);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log("An error occured while reading the imports of an RdfGraph. Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetImports(Graph graph)");
            }
            return result;
        }

        internal static string GetDataPropertyAsString(Graph graph, IcddBaseElement subject, Uri property)
        {
            string result = string.Empty;
            try
            {
                var predicateNode = graph.CreateUriNode(property);
                var subjectNode = subject.AsNode();

                var propertyNodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode).ToList();

                if (propertyNodes.Any())
                    result = propertyNodes.FirstOrDefault()?.Object.ToString();
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Property " + property.Fragment + " for  " + subject.AsResource() + " - Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetDataPropertyToString(...)");

            }
            return result;
        }

        internal static Uri GetDataPropertyAsUri(Graph graph, IcddBaseElement subject, Uri property)
        {
            Uri result = null;
            try
            {
                var predicateNode = graph.CreateUriNode(property);
                var subjectNode = subject.AsNode();

                var propertyNodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode).ToList();

                if (propertyNodes.Any())
                    result = (propertyNodes.FirstOrDefault()?.Object as UriNode)?.Uri;
                if (result == null)
                {
                    var uriLiteral = (propertyNodes.FirstOrDefault()?.Object as LiteralNode)?.Value;
                    if (!Uri.IsWellFormedUriString(uriLiteral, UriKind.Absolute))
                        return null;

                    result = new Uri(uriLiteral);
                }
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Property " + property.Fragment + " for  " + subject.AsResource() + " - Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetDataPropertyToString(...)");

            }
            return result;
        }

        internal static IcddResourceWrapper GetDataPropertyAsGraphElement(Graph graph, IcddBaseElement subject, Uri property)
        {
            IcddResourceWrapper result = null;
            try
            {
                var propertyNodes = graph.GetTriplesWithSubjectPredicate(subject.AsNode(), graph.CreateUriNode(property)).ToList();
                if (propertyNodes.Any())
                    result = new IcddResourceWrapper(propertyNodes.FirstOrDefault()?.Object as UriNode);
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Property " + property.Fragment + " for  " + subject.AsResource() + " - Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetDataPropertyToString(...)");

            }
            return result;
        }

        internal static INode GetDataPropertyAsNode(Graph graph, IcddBaseElement subject, Uri property)
        {
            INode result = null;
            try
            {
                var propertyNodes = graph.GetTriplesWithSubjectPredicate(subject.AsNode(), graph.CreateUriNode(property)).ToList();
                if (propertyNodes.Any())
                    result = propertyNodes.FirstOrDefault()?.Object;
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Property " + property.Fragment + " for  " + subject.AsResource() + " - Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetDataPropertyAsNode(...)");

            }
            return result;
        }

        internal static int? GetDataPropertyAsInt(Graph graph, IcddBaseElement subject, Uri property)
        {
            int? result = null;
            try
            {
                var predicateNode = graph.CreateUriNode(property);
                var subjectNode = subject.AsNode();


                var propertyNodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode).ToList();

                if (propertyNodes.Any())
                    result = int.Parse(propertyNodes.First().Object.GeNodeLiteral() ?? string.Empty);
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Property " + property.Fragment + " for  " + subject.AsResource() + " - Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetDataPropertyAsInt(...)");

            }
            return result;
        }

        internal static DateTime GetDataPropertyAsDateTime(Graph graph, IcddBaseElement subject, Uri property)
        {
            var result = new DateTime(2000, 1, 1);


            try
            {
                var predicateNode = graph.CreateUriNode(property);
                var subjectNode = subject.AsNode();
                var nodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode).ToList();

                if (nodes.Any())
                    result = nodes.First().Object.AsValuedNode().AsDateTime();
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Property " + property + " for  " + subject.AsResource() + " - Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetDataPropertyToDateTime(...)");
            }
            return result;
        }

        internal static bool GetDataPropertyAsBool(Graph graph, IcddBaseElement subject, Uri property)
        {

            var result = false;
            try
            {
                var predicateNode = graph.CreateUriNode(property);
                var subjectNode = subject.AsNode();

                var nodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode).ToList();
                if (nodes.Any())
                    result = nodes.First().Object.AsValuedNode().AsBoolean();
            }
            catch (Exception e)
            {
                Logger.Log("Could not find Property " + property.Fragment + " for  " + subject.AsResource() + " - Exception: " + e,
                    Logger.MsgType.Error, "RdfReader.GetDataPropertyToBool(...)");
            }
            return result;
        }
        #endregion

        #region Documents

        internal static CtDocument GetDocument(InformationContainer container, INode resource)
        {

            if (container == null || resource == null)
                return null;

            var existingDocument = container.Documents.Find(document => document.Resource.Equals(resource));
            if (existingDocument != null)
                return existingDocument;

            var type = GetInstanceType(container.IndexGraph, resource);
            CtDocument document = type switch
            {
                IcddTypeSpecsHelper.sCtInternalDocument => CtInternalDocument.Read(container, resource),
                IcddTypeSpecsHelper.sCtExternalDocument => CtExternalDocument.Read(container, resource),
                IcddTypeSpecsHelper.sCtFolderDocument => CtFolderDocument.Read(container, resource),
                IcddTypeSpecsHelper.sCtSecuredDocument => CtSecuredDocument.Read(container, resource),
                IcddTypeSpecsHelper.sCtEncryptedDocument => CtEncryptedDocument.Read(container, resource),
                IcddTypeSpecsHelper.sExtDocDatabaseLink => ExtDatabaseLink.Read(container, resource),
                IcddTypeSpecsHelper.sExtDocPayloadProxy => ExtPayloadProxy.Read(container, resource),
                _ => null
            };
            return document;
        }

        #endregion

        #region Linkset

        internal static LsIdentifier GetIdentifier(CtLinkset linkset, INode resource)
        {
            if (linkset == null || resource == null)
                return null;

            var type = GetInstanceType((Graph)resource.Graph, resource);
            LsIdentifier identifier = type switch
            {
                IcddTypeSpecsHelper.sLsStringBasedIdentifier => LsStringBasedIdentifier.Read(linkset, resource),
                IcddTypeSpecsHelper.sLsQueryBasedIdentifier => LsQueryBasedIdentifier.Read(linkset, resource),
                IcddTypeSpecsHelper.sLsURIBasedIdentifier => LsUriBasedIdentifier.Read(linkset, resource),
                _ => null
            };
            return identifier;
        }

        internal static List<LsLink> GetLinks(CtLinkset linkset)
        {
            Graph graph = linkset.LinksetGraph;
            IUriNode predicate = graph.CreateUriNode(IcddNamespacesHelper.RDF_TYPE_URI);
            List<LsLink> links = graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.LsLink)).Select(definingTriple => LsLink.Read(linkset, definingTriple.Subject)).ToList();
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.LsBinaryLink)).Select(definingTriple => LsBinaryLink.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.LsDirectedLink)).Select(definingTriple => LsDirectedLink.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.LsDirectedBinaryLink)).Select(definingTriple => LsDirectedBinaryLink.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.LsDirected1toNLink)).Select(definingTriple => LsDirected1ToNLink.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsConflictsWith)).Select(definingTriple => LsExtConflictsWith.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsControls)).Select(definingTriple => LsExtControls.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsElaborates)).Select(definingTriple => LsExtElaborates.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsHasMember)).Select(definingTriple => LsExtHasMember.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsHasPart)).Select(definingTriple => LsExtHasPart.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsAlternativeTo)).Select(definingTriple => LsExtIsAlternativeTo.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsControlledBy)).Select(definingTriple => LsExtIsControlledBy.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsElaboratedBy)).Select(definingTriple => LsExtIsElaboratedBy.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsIdenticalTo)).Select(definingTriple => LsExtIsIdenticalTo.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsMemberOf)).Select(definingTriple => LsExtIsMemberOf.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsPartOf)).Select(definingTriple => LsExtIsPartOf.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsSpecialisedAs)).Select(definingTriple => LsExtIsSpecialisedAs.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsIsSupersededBy)).Select(definingTriple => LsExtIsSupersededBy.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsSpecialises)).Select(definingTriple => LsExtSpecialises.Read(linkset, definingTriple.Subject)));
            links.AddRange(graph.GetTriplesWithPredicateObject(predicate, graph.CreateUriNode(IcddTypeSpecsHelper.ExLsSupersedes)).Select(definingTriple => LsExtSupersedes.Read(linkset, definingTriple.Subject)));
            return links;
        }

        internal static List<LsLinkElement> GetLinkElementsByLink(CtLinkset linkset, IcddBaseElement subject, Uri predicate)
        {
            if (linkset == null || subject == null || predicate == null) return new List<LsLinkElement>();
            IUriNode predicateNode = linkset.Graph.CreateUriNode(predicate);
            IEnumerable<Triple> linkElementNodes = linkset.LinksetGraph.GetTriplesWithSubjectPredicate(subject.AsNode(), predicateNode);
            return linkElementNodes.Select(element => element.Object).Select(resource => LsLinkElement.Read(linkset, resource)).ToList();
        }
        #endregion
    }
}