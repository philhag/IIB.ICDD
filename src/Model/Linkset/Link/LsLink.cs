using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Conversion.Container;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.DynamicSemantics;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset.Identifier;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace IIB.ICDD.Model.Linkset.Link
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  LsLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsEntity" />
    public class LsLink : DsEntity, IIcddVersioning, IIcddLink
    {
        internal CtLinkset Linkset;
        public string VersionId
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.VERSION_ID);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.VERSION_ID, value, AttributeType.String);
        }
        public string VersionDescription
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.VERSION_DESCRIPTION);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.VERSION_DESCRIPTION, value, AttributeType.String);
        }
        public List<LsLinkElement> HasLinkElements
        {
            get => RdfReader.GetLinkElementsByLink(Linkset, this, IcddPredicateSpecsHelper.HAS_LINK_ELEMENT);
            set
            {
                if (value.Count < 2) return;
                RdfWriter.ClearDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_LINK_ELEMENT);
                foreach (var linkElement in value)
                {
                    RdfWriter.AddDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_LINK_ELEMENT, linkElement.AsUri(), AttributeType.Uri); ;
                }
            }
        }

        internal LsLink(CtLinkset linkset, List<LsLinkElement> linkElements) : base(linkset.LinksetGraph, linkset.Container)
        {
            Linkset = linkset;
            HasLinkElements = linkElements;
        }

        protected LsLink(CtLinkset linkset, LsLinkElement element, LsLinkElement otherElement) : base(linkset.LinksetGraph, linkset.Container)
        {
            Linkset = linkset;
            HasLinkElements = new List<LsLinkElement> { element, otherElement };
        }

        protected LsLink(CtLinkset linkset, INode resource) : base(linkset.LinksetGraph, linkset.Container, resource)
        {
            Linkset = linkset;
        }

        protected LsLink(CtLinkset linkset) : base(linkset.LinksetGraph, linkset.Container)
        {
            Linkset = linkset;
        }

        public static LsLink Read(CtLinkset linkset, INode resource)
        {
            try
            {

                return new LsLink(
                    linkset ?? throw new ArgumentNullException(nameof(linkset)),
                    resource as IUriNode ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtOrganisation.Read");
                return null;
            }
        }

        public bool AddLinkElement(LsLinkElement linkElement)
        {
            var result = RdfWriter.AddDataProperty(Graph, this, IcddPredicateSpecsHelper.HAS_LINK_ELEMENT, linkElement.AsUri(), AttributeType.Uri); ;
            if (result)
                HasLinkElements.Add(linkElement);
            return result;
        }

        public bool DeleteLinkElement(LsLinkElement linkElement)
        {
            var result = RdfWriter.DeleteLinkElement(Graph, linkElement); ;
            if (result)
                HasLinkElements.Remove(linkElement);
            return result;
        }

        public override string ToString()
        {
            var s = HasLinkElements.Aggregate("Link: ", (current, elements) => current + (elements + " <-> "));
            s.Remove(s.Length - 5);
            return s;
        }

        public virtual bool RefersToDocument(CtDocument document)
        {
            if (ContainerHandler.IsSameOrSubclass(typeof(LsDirectedLink), this.GetType()))
            {
                return (this as LsDirectedLink).RefersToDocument(document);
            }
            return HasLinkElements.Any(element => Equals(element.HasDocument, document));
        }

        public virtual bool IsLinkType(Type linkType)
        {
            return ContainerHandler.IsSameClass(linkType, this.GetType());
        }
        public virtual bool IsLinkTypeOrSubtype(Type linkType)
        {
            return ContainerHandler.IsSameOrSubclass(linkType, this.GetType());
        }

        public override IGraph AsSubgraph()
        {
            Graph g = new();
            g.BaseUri = Linkset.LinksetGraph.BaseUri;
            g.Merge(base.AsSubgraph(), true);

            IcddConverter conv = new(Container);
            var ldGraph = conv.ConvertToGraph();
            g.NamespaceMap.Import(ldGraph.NamespaceMap);
            ContainerHandler.OrganizeDuplicateNamespaces(g);

            if (ContainerHandler.IsSameOrSubclass(typeof(LsDirectedLink), GetType()))
            {
                if (this is LsDirectedLink dirLink)
                {
                    dirLink?.HasLinkElements.ForEach(linkElement => g.Merge(linkElement.AsSubgraph(), true));
                    foreach (var linkElement in dirLink.HasLinkElements)
                    {
                        try
                        {
                            switch (linkElement.HasIdentifier)
                            {
                                case LsStringBasedIdentifier stringBasedIdentifier:
                                    ldGraph
                                        .GetTriplesWithObject(ldGraph.GetLiteralNode(
                                            stringBasedIdentifier.Identifier)).ToList()
                                        .ForEach(ident =>
                                            ldGraph.GetTriplesWithSubject(ident.Subject).ToList()
                                                .ForEach(trip => g.Assert(trip)));
                                    break;
                                case LsUriBasedIdentifier uriBasedIdentifier:
                                    ldGraph
                                        .GetTriplesWithObject(ldGraph.CreateUriNode(
                                            uriBasedIdentifier.Uri)).ToList()
                                        .ForEach(ident =>
                                            ldGraph.GetTriplesWithSubject(ident.Subject).ToList()
                                                .ForEach(trip => g.Assert(trip)));
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Log(e.Message, Logger.MsgType.Error, " IGraph AsSubgraph()");
                        }
                    }
                }
            }
            else
            {
                HasLinkElements.ForEach(linkElement => g.Merge(linkElement.AsSubgraph(), true));
                foreach (var linkElement in HasLinkElements)
                {
                    try
                    {
                        switch (linkElement.HasIdentifier)
                        {
                            case LsStringBasedIdentifier stringBasedIdentifier:
                                ldGraph
                                    .GetTriplesWithObject(ldGraph.CreateLiteralNode(
                                        stringBasedIdentifier.Identifier)).ToList()
                                    .ForEach(ident =>
                                        ldGraph.GetTriplesWithSubject(ident.Subject).ToList()
                                            .ForEach(trip => g.Assert(trip)));
                                break;
                            case LsUriBasedIdentifier uriBasedIdentifier:
                                ldGraph
                                    .GetTriplesWithObject(ldGraph.CreateUriNode(
                                        uriBasedIdentifier.Uri)).ToList()
                                    .ForEach(ident =>
                                        ldGraph.GetTriplesWithSubject(ident.Subject).ToList()
                                            .ForEach(trip => g.Assert(trip)));
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Log(e.Message, Logger.MsgType.Error, " IGraph AsSubgraph()");
                    }

                }

            }



            return g;
        }

        public string GetGraphJson()
        {
            GraphJsonHandling handling = new(this);
            return handling.ToJson();

        }

        public string AsTurtleString()
        {
            var graph = this.AsSubgraph();

            CompressingTurtleWriter turtleWriter = new(TurtleSyntax.W3C);
            String data = StringWriter.Write(graph, turtleWriter);

            return data;

        }

        public virtual bool IsValid()
        {
            return HasLinkElements.Count > 1;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.LsLink;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.LsLink);
        }

        public IcddBaseElement NextVersion(string versionID, string versionDescription, InformationContainer copyToContainer = null)
        {
            throw new NotImplementedException();
        }
    }
}
