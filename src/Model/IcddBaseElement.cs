using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using LibGit2Sharp;
using Newtonsoft.Json.Linq;
using VDS.RDF;

namespace IIB.ICDD.Model
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddBaseElement
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de
    /// </summary>

    public abstract class IcddBaseElement
    {
        public string Guid => Resource.GetNodeUri().Fragment.TrimStart('#');
        public string EffectiveType => GetTypeString();
        private Uri Uri => Resource.GetNodeUri();
        public InformationContainer Container { get;}
        public Graph Graph { get;}
        public INode Resource { get; }

        public DateTime Creation
        {
            get => RdfReader.GetDataPropertyAsDateTime(Graph, this, IcddPredicateSpecsHelper.CREATION_DATE);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.CREATION_DATE, value, AttributeType.DateTime, true);

        }

        public CtPerson Creator
        {
            get => CtPerson.Read(Container, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.CREATED_BY).AsNode());
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.CREATED_BY, value.AsUri(), AttributeType.Uri, true);
        }

        public DateTime Modification
        {
            get => RdfReader.GetDataPropertyAsDateTime(Graph, this, IcddPredicateSpecsHelper.MODIFICATION_DATE);
            set
            {
                IsModified = true;
                RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.MODIFICATION_DATE, value, AttributeType.DateTime, true);

            }
        }

        public bool IsModified
        {
            get => RdfReader.GetDataPropertyAsBool(Graph, this, IcddPredicateSpecsHelper.IS_MODIFIED);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.IS_MODIFIED, value, AttributeType.Bool, true);
        }

        public CtPerson Modifier
        {
            get => CtPerson.Read(Container, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.MODIFIED_BY).AsNode());
            set
            {
                IsModified = true;
                Modification = DateTime.Now;
                RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.MODIFIED_BY, value.AsUri(), AttributeType.Uri, true);
            }
        }

        public CtOrganisation Publisher
        {
            get => CtOrganisation.Read(Container, RdfReader.GetDataPropertyAsGraphElement(Graph, this, IcddPredicateSpecsHelper.PUBLISHED_BY).AsNode());
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.PUBLISHED_BY, value.AsUri(), AttributeType.Uri, true);
        }

        protected IcddBaseElement(Graph graph, InformationContainer container, INode resource)
        {
            if (resource.GetNodeUri().IsAbsoluteUri)
            {
                Graph = graph;
                Container = container;
                Resource = resource;
#if DEBUG
                Console.WriteLine("Loading " + EffectiveType + " from RDF node: " + Resource);
#endif
            }
            else
            {
                throw new ArgumentException();
            }
        }


        protected IcddBaseElement(Graph graph, InformationContainer container)
        {
            Graph = graph;
            Container = container;

            var newUri = new Uri(graph.BaseUri.AbsoluteUri + "#" + EffectiveType + "-" + ContainerHandler.Encode(System.Guid.NewGuid()));
            Resource = Graph.CreateUriNode(newUri);

            RdfWriter.SetTriple(Graph, Resource, Graph.CreateUriNode(IcddNamespacesHelper.RDF_TYPE_URI), Type());
            Creation = DateTime.Now;
#if DEBUG
            Logger.Log("Created " + EffectiveType + " from RDF node: " + Resource,Logger.MsgType.Info, "IcddBaseElement.Constructor");
#endif

        }

        public IcddResourceWrapper AsResource()
        {
            return new IcddResourceWrapper(this);
        }

        public Uri AsUri()
        {
            return Uri;
        }

        public string GetTypeString()
        {
            var type = Type();
            var typeUri = type.GetNodeUri();
            var typeFragment = typeUri.Fragment;
            var typeString = typeFragment.TrimStart('#');
            return typeString;
        }

        public INode AsNode()
        {
            return Resource;
        }


        public virtual IGraph AsSubgraph()
        {
            Graph g = new();
            g.BaseUri = Graph.BaseUri;
            var triples = Graph.GetTriplesWithSubject(AsNode());
            triples.ToList().ForEach(triple => g.Assert(triple));
            return g;
        }

        public abstract Uri ElementType();

        public abstract IUriNode Type();


        public override bool Equals(object obj)
        {
            if (obj is IcddBaseElement elem)
            {
                return Guid == elem.Guid;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return -737073652 + EqualityComparer<string>.Default.GetHashCode(Guid);
        }

        public string ToJsonLDString()
        {
            try
            {
                return ToJsonLD().ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public JObject ToJsonLD()
        {
            try
            {
                IcddJsonLdWriter writer = new(this);
                return writer.Write();
            }
            catch (Exception e)
            {
                throw new IcddException("Could not write JsonLD.", e);
            }


        }
        public bool IsSameOrSubclass(Type type)
        {
            var classType = this.GetType();
            return classType.IsSubclassOf(type)
                   || type == classType;
        }

    }
}
