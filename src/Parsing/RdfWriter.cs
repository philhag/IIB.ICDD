using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Model.Linkset.Link;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace IIB.ICDD.Parsing
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  RdfWriter 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal static class RdfWriter
    {
        /// <summary>
        /// Sets the attribute of a specific instance with a value in a specifice type.
        /// </summary>
        /// <param name="graph">The respective RDF graph.</param>
        /// <param name="subject">The unique identifier of the instance.</param>
        /// <param name="property">The respective attribute from the graph.</param>
        /// <param name="objectValue"></param>
        /// <param name="type">The type of the attribute.</param>
        /// <param name="createIfNotExist">if set to <c>true</c> creates a new attribute for this instance.</param>
        /// <returns></returns>
        internal static bool SetDataProperty(Graph graph, IcddBaseElement subject, Uri property, object objectValue, AttributeType type, bool createIfNotExist = true)
        {
            if (objectValue == null)
                return false;
            bool result = false;
            try
            {
                var predicateNode = graph.CreateUriNode(property);
                var subjectNode = subject.AsNode();
                var nameNodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode);
                var enumerable = nameNodes as IList<Triple> ?? nameNodes.ToList();

                INode objectNode;
                switch (type)
                {
                    case AttributeType.String:
                        objectNode = graph.CreateLiteralNode(objectValue as string);
                        break;
                    case AttributeType.Uri:
                        if (objectValue.GetType() == typeof(Uri))
                        {
                            objectNode = graph.CreateUriNode(objectValue as Uri);
                        }
                        else
                        {
                            if (objectValue.GetType() == typeof(IcddResourceWrapper))
                            {
                                objectNode = (objectValue as IcddResourceWrapper)?.AsNode();
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }

                        break;
                    case AttributeType.DateTime:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDateTime));
                        break;
                    case AttributeType.Integer:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeInt));
                        break;
                    case AttributeType.Bool:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeBoolean));
                        break;
                    case AttributeType.Decimal:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeBoolean));
                        break;
                    default:
                        objectNode = graph.CreateLiteralNode(objectValue as string);
                        break;
                }

                if (enumerable.Any())
                {
                    Triple t = new(subjectNode, predicateNode, objectNode);
                    graph.Retract(enumerable.First());
                    result = graph.Assert(t);
                }
                if (!result && !enumerable.Any() && createIfNotExist)
                {
                    Triple t = new(subjectNode, predicateNode, objectNode);
                    result = graph.Assert(t);
                }
                if (!property.Equals(IcddPredicateSpecsHelper.MODIFICATION_DATE)
                    && !property.Equals(IcddPredicateSpecsHelper.IS_MODIFIED)
                    && !property.Equals(IcddPredicateSpecsHelper.MODIFIED_BY))
                {
                    subject.Modification = DateTime.Now;
                }

            }
            catch (Exception e)
            {
                Logger.Log("Could not find Instance for  " + subject.AsResource() + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.SetAttribute(...)");
            }
            return result;
        }

        internal static bool AddDataProperty(Graph graph, IcddBaseElement subject, Uri property, object objectValue, AttributeType type)
        {
            bool result = false;
            try
            {
                var predicateNode = graph.CreateUriNode(property);
                var subjectNode = subject.AsNode();

                INode objectNode;
                switch (type)
                {
                    case AttributeType.String:
                        objectNode = graph.CreateLiteralNode(objectValue as string);
                        break;
                    case AttributeType.Uri:
                        if (objectValue.GetType() == typeof(Uri))
                        {
                            objectNode = graph.CreateUriNode(objectValue as Uri);
                        }
                        else
                        {
                            if (objectValue.GetType() == typeof(IcddResourceWrapper))
                            {
                                objectNode = (objectValue as IcddResourceWrapper)?.AsNode();
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }

                        break;
                    case AttributeType.DateTime:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDateTime));
                        break;
                    case AttributeType.Integer:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeInt));
                        break;
                    case AttributeType.Bool:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeBoolean));
                        break;
                    case AttributeType.Decimal:
                        objectNode = graph.CreateLiteralNode(objectValue.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeBoolean));
                        break;
                    default:
                        objectNode = graph.CreateLiteralNode(objectValue as string);
                        break;
                }


                Triple t = new(subjectNode, predicateNode, objectNode);
                result = graph.Assert(t);

            }
            catch (Exception e)
            {
                Logger.Log("Could not find Instance for  " + subject.AsResource() + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.AddDataProperty(...)");
            }
            return result;
        }

        internal static bool ClearDataProperty(Graph graph, IcddBaseElement subject, Uri property)
        {
            bool result = false;
            try
            {
                IUriNode predicateNode = graph.CreateUriNode(property);
                INode subjectNode = subject.AsNode();

                List<Triple> triples = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNode).ToList();
                graph.Retract(triples);

            }
            catch (Exception e)
            {
                Logger.Log("Could not find Instance for  " + subject.AsResource() + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.ClearDataProperty(...)");
            }
            return result;
        }

        internal static bool SetTriple(Graph graph, INode subjectNode, INode predicateNode, INode objectNode)
        {
            bool result = false;

            try
            {
                Triple t = new(subjectNode, predicateNode, objectNode);
                result = graph.Assert(t);

            }
            catch (Exception e)
            {
                Logger.Log("Could not Assert Triple - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.SetTriple(...)");
            }
            return result;
        }

        internal static void InitializeImports(Graph graph)
        {
            SetImports(graph, IcddNamespacesHelper.CONTAINER_ONTOLOGY);
            SetImports(graph, IcddNamespacesHelper.LINKSET_ONTOLOGY);
            SetImports(graph, IcddNamespacesHelper.EXT_LINKSET_ONTOLOGY);
            SetImports(graph, IcddNamespacesHelper.EXT_DOCUMENT_ONTOLOGY);
        }

        internal static void SetImports(Graph graph, Uri importedOntology)
        {
            try
            {
                var owl = RdfReader.GetNamespaceUri(graph, IcddNamespacesHelper.OWL);
                var rootNode = graph.CreateUriNode(graph.BaseUri);
                var importNode = graph.CreateUriNode(new Uri(owl + "#imports"));
                var triple = new Triple(rootNode, importNode, graph.CreateUriNode(importedOntology));
                graph.Assert(triple);
            }
            catch (Exception e)
            {
                Logger.Log("An error occured while reading the imports of an RdfGraph. Exception: " + e,
                    Logger.MsgType.Error, "IcddManagerIO.GetImports(Graph graph)");
            }
        }

        internal static bool DeleteDocument(Graph graph, CtDocument document)
        {
            var result = true;
            try
            {
                var subjectNode = document.AsResource().AsNode();
                var documentNodes = graph.GetTriplesWithSubject(subjectNode).ToList();
                documentNodes.AddRange(graph.GetTriplesWithObject(subjectNode).ToList());
                if (documentNodes.Any())
                    result &= graph.Retract(documentNodes);

                return result;
            }
            catch (Exception e)
            {
                Logger.Log("Could not delete Document " + document.Name + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.DeleteDocument(...)");
                return result;
            }
        }

        internal static bool DeleteLinkset(CtLinkset linkset)
        {
            var result = true;
            try
            {
                foreach (var link in linkset.HasLinks)
                {
                    result &= DeleteLink(linkset.LinksetGraph, link);
                }

                var subjectNode = linkset.AsNode();
                var linksetNodes = linkset.Graph.GetTriplesWithSubject(subjectNode).ToList();
                linksetNodes.AddRange(linkset.Graph.GetTriplesWithObject(subjectNode).ToList());
                if (linksetNodes.Any())
                    result &= linkset.Graph.Retract(linksetNodes);

                return result;
            }
            catch (Exception e)
            {
                Logger.Log("Could not delete Link " + linkset.FileName + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.DeleteLinkset(...)");
                return result;
            }

        }

        internal static bool DeleteLink(Graph graph, LsLink link)
        {
            var result = true;
            try
            {
                foreach (var linkElement in link.HasLinkElements)
                {
                    result &= DeleteLinkElement(graph, linkElement);
                }
                var subjectNode = link.AsNode();
                var linkNodes = graph.GetTriplesWithSubject(subjectNode).ToList();
                linkNodes.AddRange(graph.GetTriplesWithObject(subjectNode).ToList());
                if (linkNodes.Any())
                    result &= graph.Retract(linkNodes);

                return result;
            }
            catch (Exception e)
            {
                Logger.Log("Could not delete Link " + link.Guid + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.DeleteLink(...)");
                return result;
            }

        }

        internal static bool DeleteLinkElement(Graph graph, LsLinkElement linkElement)
        {
            var result = false;
            try
            {

                var subjectNode = linkElement.AsResource().AsNode();
                var triplesToDelete = graph.GetTriplesWithSubject(subjectNode).ToList();
                subjectNode = linkElement.HasIdentifier?.AsResource().AsNode();
                if (subjectNode != null)
                {
                    triplesToDelete.AddRange(graph.GetTriplesWithSubject(subjectNode).ToList());
                    triplesToDelete.AddRange(graph.GetTriplesWithObject(subjectNode).ToList());
                }

                if (triplesToDelete.Any())
                    result = graph.Retract(triplesToDelete);

                return result;
            }
            catch (Exception e)
            {
                Logger.Log("Could not delete LinkElement " + linkElement.Guid + " - Exception: " + e, Logger.MsgType.Error,
                    "IcddManagerIO.DeleteLinkElement(...)");
                return result;
            }

        }
    }

    internal enum AttributeType
    {
        String,
        Uri,
        DateTime,
        Bool,
        Integer,
        Decimal
    }
}
