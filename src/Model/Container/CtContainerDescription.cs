using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtContainerDescription 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <author>Philipp Hagedorn</author>
    public class CtContainerDescription : IcddBaseElement, IIcddVersioning
    {
        public InformationContainer Parent { get; }

        public string Description
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.DESCRIPTION);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.DESCRIPTION, value, AttributeType.String);
        }

        public string ConformanceIndicator
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.CONFORMANCE_INDICATOR);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.CONFORMANCE_INDICATOR, value, AttributeType.String);
        }

        public int? Checksum
        {
            get => RdfReader.GetDataPropertyAsInt(Graph, this, IcddPredicateSpecsHelper.CHECKSUM);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.CHECKSUM, value, AttributeType.Integer);

        }

        public string ChecksumAlgorithm
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.CHECKSUM_ALGORITHM);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.CHECKSUM_ALGORITHM, value, AttributeType.String);
        }


        public CtContainerDescription PriorVersion
        {
            get => ReadNotAppend(Parent, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.PRIOR_VERSION).AsNode());
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.PRIOR_VERSION, value.AsUri(), AttributeType.Uri, true);
        }

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

        public ICollection<CtDocument> ContainsDocument { get; }

        public ICollection<CtLinkset> ContainsLinkset { get; }

        public ICollection<CtParty> ContainsParty { get; }

        public Dictionary<CtDocument, List<LsLinkElement>> ContainsLinkElement { get; }


        public CtContainerDescription(InformationContainer container, string description, string versionId,
            string versionDescription) : base(container.IndexGraph ?? throw new ArgumentNullException(nameof(container)), container)
        {
            VersionId = versionId ?? throw new ArgumentNullException(nameof(versionId));
            VersionDescription = versionDescription ?? throw new ArgumentNullException(nameof(versionDescription));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ConformanceIndicator = IcddConformanceTypesHelper.Part1;
            Parent = container;
            ContainsParty = new List<CtParty>();
            ContainsDocument = new List<CtDocument>();
            ContainsLinkElement = new Dictionary<CtDocument, List<LsLinkElement>>();
            ContainsLinkset = new List<CtLinkset>();
        }

        private CtContainerDescription(InformationContainer informationContainer, INode resource) :
            base(informationContainer.IndexGraph, informationContainer, resource)
        {
            Parent = informationContainer;
            ContainsParty = new List<CtParty>();
            ContainsDocument = new List<CtDocument>();
            ContainsLinkset = new List<CtLinkset>();
            ContainsLinkElement = new Dictionary<CtDocument, List<LsLinkElement>>();
        }

        public static CtContainerDescription Read(InformationContainer informationContainer, INode resource)
        {
            try
            {
                informationContainer.ContainerDescription = new CtContainerDescription(
                    informationContainer,
                    resource ?? throw new ArgumentNullException(nameof(resource)));

                CtContainerDescription description = informationContainer.ContainerDescription;

                foreach (INode person in RdfReader.GetInstances(description.Graph, IcddTypeSpecsHelper.CtPerson))
                    description.ContainsParty.Add(CtPerson.Read(description.Parent, person));

                foreach (INode organisation in RdfReader.GetInstances(description.Graph, IcddTypeSpecsHelper.CtOrganisation))
                    description.ContainsParty.Add(CtOrganisation.Read(description.Parent, organisation));


                foreach (INode document in RdfReader.GetInstances(description.Graph, description.AsResource(), IcddPredicateSpecsHelper.CONTAINS_DOCUMENT))
                    description.ContainsDocument.Add(RdfReader.GetDocument(description.Parent, document));

                foreach (INode linkset in RdfReader.GetInstances(description.Graph, description.AsResource(), IcddPredicateSpecsHelper.CONTAINS_LINKSET))
                    description.ContainsLinkset.Add(CtLinkset.Read(description.Parent, linkset));

                return informationContainer.ContainerDescription;

            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtContainerDescription.Read");
                return null;
            }
        }

        internal static CtContainerDescription ReadNotAppend(InformationContainer informationContainer, INode resource)
        {
            try
            {
                CtContainerDescription description = new(
                    informationContainer ?? throw new ArgumentNullException(nameof(informationContainer)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));

                foreach (INode person in RdfReader.GetInstances(description.Graph, IcddTypeSpecsHelper.CtPerson))
                    description.ContainsParty.Add(CtPerson.Read(description.Parent, person));

                foreach (INode organisation in RdfReader.GetInstances(description.Graph, IcddTypeSpecsHelper.CtOrganisation))
                    description.ContainsParty.Add(CtOrganisation.Read(description.Parent, organisation));
                

                foreach (INode document in RdfReader.GetInstances(description.Graph, description.AsResource(), IcddPredicateSpecsHelper.CONTAINS_DOCUMENT))
                    description.ContainsDocument.Add(RdfReader.GetDocument(description.Parent, document));

                foreach (INode linkset in RdfReader.GetInstances(description.Graph, description.AsResource(), IcddPredicateSpecsHelper.CONTAINS_LINKSET))
                    description.ContainsLinkset.Add(CtLinkset.Read(description.Parent, linkset));

                return description;
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtContainerDescription.ReadNotAppend");
                return null;
            }
        }


        public List<LsLinkElement> GetLinkElementsByDocument(CtDocument document)
        {
            var result = new List<LsLinkElement>();
            if (ContainsLinkElement.TryGetValue(document, out List<LsLinkElement> linkElements))
            {
                result = linkElements;
            }
            return result;

        }

        internal bool AddDocument(CtDocument document)
        {
            ContainsDocument.Add(document);
            Modification = DateTime.Now;
            return RdfWriter.AddDataProperty(Graph, this, IcddPredicateSpecsHelper.CONTAINS_DOCUMENT, document.AsUri(), AttributeType.Uri);
        }

        internal bool AddLinkset(CtLinkset linkset)
        {
            ContainsLinkset.Add(linkset);
            Modification = DateTime.Now;
            return RdfWriter.AddDataProperty(Graph, this, IcddPredicateSpecsHelper.CONTAINS_LINKSET, linkset.AsUri(), AttributeType.Uri);
        }

        public void AddLinkElements(CtLinkset linkSet)
        {
            foreach (var link in linkSet.HasLinks)
            {
                foreach (var linkElement in link.HasLinkElements)
                {
                    var objList = new List<LsLinkElement>();
                    objList.AddRange(link.HasLinkElements);
                    objList.Remove(linkElement);

                    if (!ContainsLinkElement.ContainsKey(linkElement.HasDocument))
                    {
                        ContainsLinkElement.Add(linkElement.HasDocument, objList);
                    }
                    else
                    {
                        ContainsLinkElement.TryGetValue(linkElement.HasDocument, out List<LsLinkElement> list);
                        list?.AddRange(objList);
                    }
                }
            }
        }

        public CtPerson AddPerson(string name, string description)
        {
            var person = new CtPerson(Container, name, description);
            ContainsParty.Add(person);
            Modification = DateTime.Now;
            return person;
        }

        public CtOrganisation AddOrganisation(string name, string description)
        {
            var organisation = new CtOrganisation(Container, name, description);
            ContainsParty.Add(organisation);
            Modification = DateTime.Now;
            return organisation;
        }

        public void AddPerson(CtPerson person)
        {
            ContainsParty.Add(person);
        }

        public void AddOrganisation(CtOrganisation organisation)
        {
            ContainsParty.Add(organisation);
        }

        public void AddParty(CtParty party)
        {
            ContainsParty.Add(party);
        }

        public CtParty GetPartyById(string id)
        {
            return ((List<CtParty>)ContainsParty).Find(party => party.Guid.Equals(id));
        }

        public CtPerson GetPersonById(string id)
        {
            return GetPartyById(id) as CtPerson;
        }

        public CtOrganisation GetOrganisationById(string id)
        {
            return GetPartyById(id) as CtOrganisation;
        }


        public CtDocument GetDocument(string id)
        {
            return ((List<CtDocument>)ContainsDocument).Find(document => document.Guid.Equals(id));
        }

        public CtLinkset GetLinkset(string id)
        {
            return ((List<CtLinkset>)ContainsLinkset).Find(linkset => linkset.Guid.Equals(id));
        }
        public bool HasDocument(string filename)
        {
            return ((List<CtDocument>)ContainsDocument).Find(document => document.Name.Equals(filename)) != null
                   || ((List<CtDocument>)ContainsDocument).Find(document => ((CtInternalDocument)document).FileName.Equals(filename)) != null;
        }

        public bool HasLinkset(string name)
        {
            return ((List<CtLinkset>)ContainsLinkset).Find(linkset => linkset.FileName.Equals(name)) != null;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtContainerDescription;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtContainerDescription);
        }
        public IcddBaseElement NextVersion(string versionID, string versionDescription, InformationContainer appendToContainer = null)
        {
            if (appendToContainer != null)
            {
                appendToContainer.ContainerDescription = new CtContainerDescription(appendToContainer, Description, versionID, versionDescription)
                    {
                        PriorVersion = this
                    };
                foreach (var party in ContainsParty)
                {
                    appendToContainer.ContainerDescription.AddParty(party);
                }
                foreach (var document in ContainsDocument)
                {
                    appendToContainer.ContainerDescription.AddDocument(document);
                }
                foreach (var linkset in ContainsLinkset)
                {
                    appendToContainer.ContainerDescription.AddLinkset(linkset);
                    appendToContainer.ContainerDescription.AddLinkElements(linkset);
                }
                return appendToContainer.ContainerDescription;
            }

            Parent.ContainerDescription = new CtContainerDescription(Parent, Description, versionID, versionDescription)
            {
                PriorVersion = this
            };
            foreach (var party in ContainsParty)
            {
                Parent.ContainerDescription.AddParty(party);
            }
            foreach (var document in ContainsDocument)
            {
                Parent.ContainerDescription.AddDocument(document);
            }
            foreach (var linkset in ContainsLinkset)
            {
                Parent.ContainerDescription.AddLinkset(linkset);
                Parent.ContainerDescription.AddLinkElements(linkset);
            }
            return Parent.ContainerDescription;
        }
    }
}