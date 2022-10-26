using System;
using System.Collections.Generic;
using IIB.ICDD.Handling;
using VDS.RDF;
using VDS.RDF.Ontology;

namespace IIB.ICDD.Model
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddResourceWrapper 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddResourceWrapper : OntologyResource
    {
        public string Namespace => Uri.GetLeftPart(UriPartial.Path);
        public string Identifier => Uri.Fragment.TrimStart('#');
        public Uri Uri => AsNode().Uri;

        public IcddResourceWrapper(IcddBaseElement wrapBaseElement) : base(wrapBaseElement.Graph.CreateUriNode(wrapBaseElement.AsUri()),
            wrapBaseElement.Graph)
        {

        }
        public IcddResourceWrapper(Uri uri, IGraph graph) : base(graph.CreateUriNode(uri), graph)
        {
        }

        public IcddResourceWrapper(string ns, string type, IGraph graph) : base(graph.CreateUriNode(IcddNodeHandler.CreateUriWithFragment(ns, type)), graph)
        {

        }

        public IcddResourceWrapper(INode node) : base(node, node.Graph)
        {
        }

        //public IcddResourceWrapper(string ns, string type)
        //{
        //    Namespace = ns.TrimEnd('#');
        //    Identifier = type;
        //    Uri = !string.IsNullOrEmpty(type) ? new Uri(Namespace + "#" + Identifier) : new Uri(Namespace);


        //}

        //public IcddResourceWrapper(Uri ns, string type)
        //{
        //    Namespace = ns.AbsoluteUri.TrimEnd('#');
        //    Identifier = type;
        //    Uri = new Uri(Namespace + "#" + Identifier);
        //}
        //public IcddResourceWrapper(string ns)
        //{
        //    Namespace = ns;
        //    Identifier = "";
        //    Uri = new Uri(Namespace);
        //}
        //public IcddResourceWrapper(Uri uri)
        //{
        //    Namespace = uri.GetLeftPart(UriPartial.Path);
        //    Identifier = uri.Fragment.TrimStart('#');
        //    Uri = uri;
        //}
        //public IcddResourceWrapper(IUriNode uriNode)
        //{
        //    Namespace = uriNode.Uri.GetLeftPart(UriPartial.Path);
        //    Identifier = uriNode.Uri.Fragment.TrimStart('#');
        //    Uri = uriNode.Uri;
        //}

        public IUriNode AsNode()
        {
            return _resource as UriNode;
        }

        public Uri GetNamespace()
        {
            return new Uri(Namespace + "#");
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is IcddResourceWrapper elem)
            {
                if (elem.Uri.Equals(Uri) && elem.Identifier.Equals(Identifier))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 808637395;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Namespace);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Identifier);
            hashCode = hashCode * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(Uri);
            return hashCode;
        }
    }
}
