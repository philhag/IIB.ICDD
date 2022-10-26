using System;
using System.IO;
using IIB.ICDD.Model;
using VDS.RDF;

namespace IIB.ICDD.Parsing.Vocabulary
{

    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddNamespacesHelper 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// 
    public static class IcddNamespacesHelper
    {
        public static string BASE_URI => "https://icdd.vm.rub.de/data/";
        public static string BASE => "";
        public static string CT => "ct";
        public static Uri CONTAINER_URI_DRAFT => new("http://www.iso-icdd.org/draft/Container.rdf#");
        public static Uri CONTAINER_URI => new("https://standards.iso.org/iso/21597/-1/ed-1/en/Container#");
        public static Uri CONTAINER_ONTOLOGY => new("https://standards.iso.org/iso/21597/-1/ed-1/en/Container.rdf");
        public static Uri CONTAINER_SHACL = new("https://standards.iso.org/iso/21597/-2/ed-1/en/AnnexB/Container.shapes.ttl");
        public static string LS => "ls";
        public static Uri LINKSET_URI_DRAFT => new("http://www.iso-icdd.org/draft/Linkset.rdf#");
        public static Uri LINKSET_URI => new("https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset#");
        public static Uri LINKSET_ONTOLOGY => new("https://standards.iso.org/iso/21597/-1/ed-1/en/Linkset.rdf");

        public static string ELS => "els";
        public static Uri EXT_LINKSET_URI => new("https://standards.iso.org/iso/21597/-2/ed-1/en/ExtendedLinkset#");
        public static Uri EXT_LINKSET_ONTOLOGY => new("https://standards.iso.org/iso/21597/-2/ed-1/en/ExtendedLinkset.rdf");

        public static string EXTDOC => "exdc";
        public static Uri EXT_DOCUMENT_URI => new("https://icdd.vm.rub.de/ontology/icdd/ExtendedDocument#");
        public static Uri EXT_DOCUMENT_ONTOLOGY => new("https://icdd.vm.rub.de/ontology/icdd/ExtendedDocument.rdf");


        public static string DS => "ds";
        public static Uri SEMANTICS_URI => new("https://standards.iso.org/iso/21597/-2/ed-1/en/DynamicSemantics#");
        public static string RDF => "rdf";
        public static Uri RDF_URI => new("http://www.w3.org/1999/02/22-rdf-syntax-ns#");
        public static Uri RDF_TYPE_URI => new("http://www.w3.org/1999/02/22-rdf-syntax-ns#type");
        public static string OWL => "owl";
        public static Uri OWL_URI => new("http://www.w3.org/2002/07/owl#");
        public static string RDFS => "rdfs";
        public static Uri RDFS_URI => new("http://www.w3.org/2000/01/rdf-schema#");
        public static string XSD => "xsd";
        public static Uri XSD_URI => new("http://www.w3.org/2001/XMLSchema#");

        public static INamespaceMapper ICDD_NAMESPACE_MAP
        {
            get
            {
                var ns = new NamespaceMapper();
                ns.AddNamespace(RDF, RDF_URI);
                ns.AddNamespace(OWL, OWL_URI);
                ns.AddNamespace(RDFS, RDFS_URI);
                ns.AddNamespace(XSD, XSD_URI);
                ns.AddNamespace(CT, CONTAINER_URI);
                ns.AddNamespace(LS, LINKSET_URI);
                ns.AddNamespace(ELS, EXT_LINKSET_URI);
                ns.AddNamespace(EXTDOC, EXT_DOCUMENT_URI);
                ns.AddNamespace("lse", new Uri("https://icdd.vm.rub.de/ontology/icdd/LinksetSparqlExtension#"));
                return ns;
            }
        }
        public static INamespaceMapper RDF_NAMESPACE_MAP
        {
            get
            {
                var ns = new NamespaceMapper();
                ns.AddNamespace(RDF, RDF_URI);
                ns.AddNamespace(OWL, OWL_URI);
                ns.AddNamespace(RDFS, RDFS_URI);
                ns.AddNamespace(XSD, XSD_URI);
                return ns;
            }
        }


        public static string BaseNamespaceFor(InformationContainer container, string type, string filename)
        {
            if (container.IndexGraph.BaseUri != null)
            {
                return Path.Combine(container.IndexGraph.BaseUri.AbsoluteUri.TrimEnd('#'), type, filename);
            }
            var name = Path.ChangeExtension(container.ContainerName, null);
            string dtString = DateTime.Now.Year + "" + DateTime.Now.Day + DateTime.Now.Month + new Random().Next(1000).ToString();
            return Path.Combine(BASE_URI, name + "-" + dtString, type, filename);
        }
        public static string BaseNamespaceFor(string containerName, string type, string filename)
        {
            string dtString = DateTime.Now.Year + "" + DateTime.Now.Day + DateTime.Now.Month + new Random().Next(1000).ToString();
            return Path.Combine(BASE_URI, containerName + "-" + dtString, type, filename);
        }

    }
}
