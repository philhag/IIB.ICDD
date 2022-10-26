using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IIB.ICDD.Handling;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.ExtendedLinkset;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Model.Linkset.Identifier;
using IIB.ICDD.Model.Linkset.Link;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using IIB.ICDD.Validation;
using Newtonsoft.Json.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Algebra;
using VDS.RDF.Writing;
using Graph = VDS.RDF.Graph;

namespace IIB.ICDD.Model.Container
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtLinkset 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class CtLinkset : IcddBaseElement, IIcddVersioning, IRdfFile
    {

        #region DataMember
        private Graph _linksetGraph;
        private List<LsLink> _hasLinks;
        private bool _isInit = false;
        private string _location;

        public string VersionId
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.VERSION_ID);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.VERSION_ID, value, AttributeType.String);
        }

        public string VersionDescription
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.VERSION_DESCRIPTION);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.VERSION_DESCRIPTION, value, AttributeType.String, true);
        }

        public string FileName
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.FILE_NAME);
            set
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FILE_NAME, value, AttributeType.String, true);
                }
                else
                {
                    var locationOld = _location;
                    var locationNew = Path.Combine(Container.GetLinksetFolder(), value);
                    locationNew = Path.ChangeExtension(locationNew, "rdf");
                    if (File.Exists(locationOld) && !File.Exists(locationNew))
                    {
                        File.Move(locationOld, locationNew);
                        if (File.Exists(locationNew))
                        {
                            _location = locationNew;
                            RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.FILE_NAME, value, AttributeType.String, true);
                        }

                    }
                }

            }
        }

        public CtLinkset PriorVersion
        {
            get => Container.Linksets.Find(ls => ls.Guid.Equals(RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.PRIOR_VERSION).Identifier));
            set
            {
                if (value != null)
                    RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.PRIOR_VERSION, value.AsUri(), AttributeType.Uri, true);
            }
        }

        public CtContainerDescription BelongsToContainer { get; private set; }

        public LsBase BaseNode { get; private set; }

        public List<LsLink> HasLinks
        {
            get
            {
                if (_hasLinks != null)
                    return _hasLinks;

                _hasLinks = new List<LsLink>();
                _hasLinks = RdfReader.GetLinks(this);
                BelongsToContainer.AddLinkElements(this);
                return _hasLinks;
            }
        }
        public Graph LinksetGraph
        {
            get => _isInit ? _linksetGraph : null;
            set
            {
                if (value == null) return;
                _linksetGraph = value;
                _isInit = true;

            }
        }


        #endregion

        #region Constructors

        internal CtLinkset(InformationContainer container, CtLinkset priorVersion, string filename) :
            base(container.IndexGraph, container)
        {
            var destinationFilePath = Path.Combine(container.GetLinksetFolder(), Path.ChangeExtension(filename, "rdf"));

            if (FileHandler.FileExists(destinationFilePath, out string newDest, out string newFilename))
            {
                destinationFilePath = newDest;
            }

            FileName = Path.GetFileName(destinationFilePath);
            BelongsToContainer = container.ContainerDescription;
            PriorVersion = priorVersion;
            _linksetGraph = new Graph();
            _location = destinationFilePath;
            _isInit = true;


            var baseNs = IcddNamespacesHelper.BaseNamespaceFor(container, "ls", Path.ChangeExtension(filename, null));

            LinksetGraph.NamespaceMap.AddNamespace("idx", new Uri(container.IndexGraph.BaseUri.AbsoluteUri + "#"));
            LinksetGraph.NamespaceMap.Import(IcddNamespacesHelper.ICDD_NAMESPACE_MAP);
            //var prefix = filename.Substring(0, 3) + new Random().Next(10);
            LinksetGraph.NamespaceMap.AddNamespace("", new Uri(baseNs + "#"));
            LinksetGraph.BaseUri = new Uri(baseNs);


            BaseNode = new LsBase(LinksetGraph, container) { Modification = DateTime.Now };
            Creation = DateTime.Now;
            Modification = DateTime.Now;
            SaveRdf();

        }
        private CtLinkset(InformationContainer container, INode resource) : base(container.IndexGraph, container, resource)
        {
            BelongsToContainer = container.ContainerDescription;
            _location = Path.Combine(container.GetLinksetFolder(), FileName);
            if (!_isInit)
            {
                if (File.Exists(_location))
                {
                    Graph linksetGraph = new();
                    FileLoader.Load(linksetGraph, _location);
                    LinksetGraph = linksetGraph;
                }

            }

        }

        public async Task Init()
        {

            await Task.Run(() =>
            {
                if (File.Exists(_location))
                {
                    Graph linksetGraph = new();
                    FileLoader.Load(linksetGraph, _location);
                    LinksetGraph = linksetGraph;
                }

            });


        }


        public static CtLinkset Read(InformationContainer container, INode resource)
        {
            try
            {
                var linkset = new CtLinkset(
                  container ?? throw new ArgumentNullException(nameof(container)),
                  resource ?? throw new ArgumentNullException(nameof(resource)));
                return linkset;

            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtLinkset.Read");
                return null;
            }

        }


        #endregion

        #region Functions
        public bool Delete()
        {
            if (!File.Exists(Location())) return false;

            if (!RdfWriter.DeleteLinkset(this)) return false;

            File.Delete(Location());
            return Container.Linksets.Remove(this);
        }
        public bool Clear()
        {
            bool success = HasLinks.Aggregate(true, (current, link) => current & DeleteLink(link));
            if (success)
                HasLinks.Clear();
            return success;
        }

        public bool AddLink(LsLink link)
        {
            if (link != null)
            {
                HasLinks.Add(link);
                return true;
            }
            return false;
        }

        public LsLink CreateLink(List<LsLinkElement> linkElements)
        {
            var link = new LsLink(this, linkElements);
            return AddLink(link) ? link : null;
        }
        public LsBinaryLink CreateBinaryLink(LsLinkElement firstLinkElement, LsLinkElement secondLinkElement)
        {
            if (firstLinkElement == null || secondLinkElement == null)
                return null;
            var link = new LsBinaryLink(this, firstLinkElement, secondLinkElement);
            return AddLink(link) ? link : null;
        }

        public LsDirectedLink CreateDirectedLink(List<LsLinkElement> fromLinkElements, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElements == null || toLinkElements == null || !fromLinkElements.Any() || !toLinkElements.Any())
                return null;

            var link = new LsDirectedLink(this, fromLinkElements, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsDirectedBinaryLink CreateDirectedBinaryLink(LsLinkElement fromLinkElement, LsLinkElement toLinkElement)
        {
            if (fromLinkElement == null || toLinkElement == null)
                return null;

            var link = new LsDirectedBinaryLink(this, fromLinkElement, toLinkElement);
            return AddLink(link) ? link : null;
        }

        public LsDirected1ToNLink CreateDirected1ToNLink(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {

            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsDirected1ToNLink(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtConflictsWith CreateConflictsWith(LsLinkElement fromLinkElement, LsLinkElement toLinkElement)
        {
            if (fromLinkElement == null || toLinkElement == null)
                return null;

            var link = new LsExtConflictsWith(this, fromLinkElement, toLinkElement);
            return AddLink(link) ? link : null;
        }

        public LsExtIsAlternativeTo CreateIsAlternativeTo(LsLinkElement fromLinkElement, LsLinkElement toLinkElement)
        {
            if (fromLinkElement == null || toLinkElement == null)
                return null;

            var link = new LsExtIsAlternativeTo(this, fromLinkElement, toLinkElement);
            return AddLink(link) ? link : null;
        }
        public LsExtIsIdenticalTo CreateIsIdenticalTo(LsLinkElement fromLinkElement, LsLinkElement toLinkElement)
        {
            if (fromLinkElement == null || toLinkElement == null)
                return null;

            var link = new LsExtIsIdenticalTo(this, fromLinkElement, toLinkElement);
            return AddLink(link) ? link : null;
        }

        public LsExtControls CreateControls(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtControls(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtElaborates CreateElaborates(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtElaborates(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtHasMember CreateHasMember(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtHasMember(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtHasPart CreateHasPart(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtHasPart(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtIsControlledBy CreateIsControlledBy(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtIsControlledBy(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtIsElaboratedBy CreateIsElaboratedBy(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtIsElaboratedBy(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtIsMemberOf CreateIsMemberOf(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtIsMemberOf(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtIsPartOf CreateIsPartOf(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtIsPartOf(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }
        public LsExtIsSpecialisedAs CreateIsSpecialisedAs(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtIsSpecialisedAs(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }
        public LsExtIsSupersededBy CreateIsSupersededBy(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtIsSupersededBy(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtSpecialises CreateSpecialises(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtSpecialises(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsExtSupersedes CreateSupersedes(LsLinkElement fromLinkElement, List<LsLinkElement> toLinkElements)
        {
            if (fromLinkElement == null || toLinkElements == null || !toLinkElements.Any())
                return null;

            var link = new LsExtSupersedes(this, fromLinkElement, toLinkElements);
            return AddLink(link) ? link : null;
        }

        public LsLinkElement CreateLinkElement(CtDocument document)
        {
            return new LsLinkElement(this, document);
        }

        public LsLinkElement CreateLinkElement(CtDocument document, LsIdentifier identifier)
        {
            return new LsLinkElement(this, document, identifier);
        }

        public LsLinkElement CreateLinkElement(CtDocument document, string identifier, string field)
        {
            var stringBasedIdentifier = CreateStringBasedIdentifier(identifier, field);
            return new LsLinkElement(this, document, stringBasedIdentifier);
        }

        public LsLinkElement CreateLinkElement(CtDocument document, Uri uri)
        {
            var uriBasedIdentifier = CreateUriBasedIdentifier(uri);
            return new LsLinkElement(this, document, uriBasedIdentifier);
        }

        public LsUriBasedIdentifier CreateUriBasedIdentifier(Uri uri)
        {
            return new LsUriBasedIdentifier(this, uri);
        }
        public LsStringBasedIdentifier CreateStringBasedIdentifier(string identifier, string identifierField)
        {
            return new LsStringBasedIdentifier(this, identifierField, identifier);
        }

        public LsQueryBasedIdentifier CreateQueryBasedIdentifier(string queryExpression, string queryLanguage)
        {
            return new LsQueryBasedIdentifier(this, queryExpression, queryLanguage);
        }

        public bool DeleteLink(LsLink link)
        {
            HasLinks.Remove(link);
            return link != null && RdfWriter.DeleteLink(LinksetGraph, link);
        }

        public bool DeleteLink(List<LsLink> links)
        {
            return links.Aggregate(true, (current, link) => current & DeleteLink(link));
        }

        public List<LsLink> GetLinksFor(CtDocument document)
        {
            return HasLinks.FindAll(link => link.RefersToDocument(document)).Distinct().ToList();
        }

        public LsLink GetLink(string id)
        {
            return HasLinks.Find(link => link.Guid == id);
        }


        public bool SaveRdf()
        {
            PrettyRdfXmlWriter writer = new(10);
            if (string.IsNullOrEmpty(_location)) return false;
            writer.Save(LinksetGraph, _location);
            return true;
        }

        internal string Location()
        {
            return _location;
        }
        #endregion

        #region Versioning
        public bool SetVersionDescription(string value)
        {
            VersionDescription = value;
            return true;
        }

        public bool SetVersionID(string value)
        {
            VersionId = value;
            return true;
        }

        public string GetVersionID()
        {
            return VersionId;
        }


        public string GetVersionDescription()
        {
            return VersionDescription;
        }

        #endregion

        #region JsonLD
        public List<LsLink> CreateLinksFromJsonLD(JObject inputData)
        {
            JsonLdParser parser = new();
            TripleStore store = new();
            StringReader sr = new(inputData.ToString());
            parser.Load(store, sr);

            Graph g = new();
            g.Assert(store.Triples);

            IcddShaclValidator val = new(g);
            if (val.Conforms())
            {
                var list = new List<LsLink>();
                IUriNode predicate = LinksetGraph.CreateUriNode(IcddNamespacesHelper.RDF_TYPE_URI);
                IEnumerable<Triple> triples = g.GetTriplesWithPredicate(predicate).ToList();

                foreach (Triple triple in triples)
                {
                    if (LinksetGraph.ContainsTriple(triple))
                        continue;
                    

                    IUriNode subjectNode = triple.Subject as IUriNode;
                    IUriNode objectNode = triple.Object as IUriNode;
                    string typeElement = new IcddResourceWrapper(objectNode).Identifier;
                    IEnumerable<Triple> relatedTriples = g.GetTriplesWithSubject(subjectNode);
                    LinksetGraph.Assert(relatedTriples);

                    switch (typeElement)
                    {
                        case IcddTypeSpecsHelper.sExLsElaborates:
                            AddLink(LsExtElaborates.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsIdenticalTo:
                            AddLink(LsExtIsIdenticalTo.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsHasPart:
                            AddLink(LsExtHasPart.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsPartOf:
                            AddLink(LsExtIsPartOf.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsControls:
                            AddLink(LsExtControls.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsElaboratedBy:
                            AddLink(LsExtIsElaboratedBy.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsSpecialises:
                            AddLink(LsExtSpecialises.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsSupersedes:
                            AddLink(LsExtSupersedes.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsAlternativeTo:
                            AddLink(LsExtIsAlternativeTo.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsMemberOf:
                            AddLink(LsExtIsMemberOf.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsSpecialisedAs:
                            AddLink(LsExtIsSpecialisedAs.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsControlledBy:
                            AddLink(LsExtIsControlledBy.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsIsSupersededBy:
                            AddLink(LsExtIsSupersededBy.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsHasMember:
                            AddLink(LsExtHasMember.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sExLsConflictsWith:
                            AddLink(LsExtConflictsWith.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sLsLink:
                            AddLink(LsLink.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sLsBinaryLink:
                            AddLink(LsBinaryLink.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sLsDirectedLink:
                            AddLink(LsDirectedLink.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sLsDirectedBinaryLink:
                            AddLink(LsDirectedBinaryLink.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sLsDirected1toNLink:
                            AddLink(LsDirected1ToNLink.Read(this, subjectNode));
                            break;
                        case IcddTypeSpecsHelper.sLsIdentifier:
                            break;
                        case IcddTypeSpecsHelper.sLsLinkElement:
                            break;
                        case IcddTypeSpecsHelper.sLsQueryBasedIdentifier:
                            break;
                        case IcddTypeSpecsHelper.sLsStringBasedIdentifier:
                            break;
                        case IcddTypeSpecsHelper.sLsURIBasedIdentifier:
                            break;
                        default:
                            break;
                    }

                   
                }

                 return list;
            }
            return new List<LsLink>();
        }
        #endregion

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtLinkset;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtLinkset);
        }

        public IcddBaseElement NextVersion(string versionID, string versionDescription, InformationContainer copyToContainer = null)
        {
            throw new NotImplementedException();
        }
    }
}