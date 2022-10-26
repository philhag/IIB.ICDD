using System;
using IIB.ICDD.Handling;
using VDS.RDF;

namespace IIB.ICDD.Parsing.Vocabulary
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddTypeSpecsHelper 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public static class IcddTypeSpecsHelper
    {
        #region Container

        public static Uri CtContainerDescription => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtContainerDescription);
        public const string sCtContainerDescription = "ContainerDescription";
        public static Uri CtEncryptedDocument => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtEncryptedDocument);
        public const string sCtEncryptedDocument = "EncryptedDocument";
        public static Uri CtExternalDocument => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtExternalDocument);
        public const string sCtExternalDocument = "ExternalDocument";
        public static Uri CtInternalDocument => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtInternalDocument);
        public const string sCtInternalDocument = "InternalDocument";
        public static Uri CtDocument => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtDocument);
        public const string sCtDocument = "Document";
        public static Uri CtSecuredDocument => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtSecuredDocument);
        public const string sCtSecuredDocument = "SecuredDocument";
        public static Uri CtFolderDocument => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtFolderDocument);
        public const string sCtFolderDocument = "FolderDocument";

        public static Uri CtLinkset = IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtLinkset);
        public const string sCtLinkset = "Linkset";
        public static Uri CtParty => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtParty);
        public const string sCtParty = "Party";
        public static Uri CtPerson => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtPerson);
        public const string sCtPerson = "Person";
        public static Uri CtOrganisation => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, sCtOrganisation);
        public const string sCtOrganisation = "Organisation";
        #endregion

        #region Linkset

        public static Uri LsLink => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsLink);
        public const string sLsLink = "Link";
        public static Uri LsBinaryLink => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsBinaryLink);
        public const string sLsBinaryLink = "BinaryLink";
        public static Uri LsDirectedLink => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsDirectedLink);
        public const string sLsDirectedLink = "DirectedLink";
        public static Uri LsDirectedBinaryLink => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsDirectedBinaryLink);
        public const string sLsDirectedBinaryLink = "DirectedBinaryLink";
        public static Uri LsDirected1toNLink => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsDirected1toNLink);
        public const string sLsDirected1toNLink = "Directed1toNLink";
        public static Uri LsIdentifier => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsIdentifier);
        public const string sLsIdentifier = "Identifier";
        public static Uri LsLinkElement => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsLinkElement);
        public const string sLsLinkElement = "LinkElement";
        public static Uri LsQueryBasedIdentifier => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsQueryBasedIdentifier);
        public const string sLsQueryBasedIdentifier = "QueryBasedIdentifier";
        public static Uri LsStringBasedIdentifier => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsStringBasedIdentifier);
        public const string sLsStringBasedIdentifier = "StringBasedIdentifier";
        public static Uri LsURIBasedIdentifier => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, sLsURIBasedIdentifier);
        public const string sLsURIBasedIdentifier = "URIBasedIdentifier";

        #endregion

        #region Semantics
        public static Uri INFORMATION_Model => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "InformationModel");
        public static Uri CONCEPT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "Concept");
        public static Uri COMPLEX_PROPERTY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "ComplexProperty");
        public static Uri COMPLEX_PROPERTY_VALUE => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "ComplexPropertyValue");
        public static Uri BOOLEAN_PROPERTY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "BooleanProperty");
        public static Uri DATETIME_PROPERTY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "DateTimeProperty");
        public static Uri STRING_PROPERTY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "StringProperty");
        public static Uri FLOAT_PROPERTY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "FloatProperty");
        public static Uri INTEGER_PROPERTY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "IntegerProperty");
        public static Uri URI_PROPERTY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "UriProperty");
        public static Uri CONTAINS_RELATION => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "ContainsRelation");
        public static Uri CONTAINS_RELATION_GROUP => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "ContainsRelationGroup");
        public static Uri RELATION => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.SEMANTICS_URI.AbsoluteUri, "Relation");


        #endregion

        #region ExtendedLinkset
        public static Uri ExLsElaborates => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsElaborates);
        public const string sExLsElaborates = "Elaborates";
        public static Uri ExLsIsIdenticalTo => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsIdenticalTo);
        public const string sExLsIsIdenticalTo = "IsIdenticalTo";
        public static Uri ExLsHasPart => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsHasPart);
        public const string sExLsHasPart = "HasPart";
        public static Uri ExLsIsPartOf => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsPartOf);
        public const string sExLsIsPartOf = "IsPartOf";
        public static Uri ExLsControls => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsControls);
        public const string sExLsControls = "Controls";

        public const string sExLsIsElaboratedBy = "IsElaboratedBy";
        public static Uri ExLsIsElaboratedBy => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsElaboratedBy);

        public const string sExLsSpecialises = "Specialises";
        public static Uri ExLsSpecialises => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsSpecialises);


        public static Uri ExLsSupersedes => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsSupersedes);
        public const string sExLsSupersedes = "Supersedes";
        public static Uri ExLsIsAlternativeTo => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsAlternativeTo);
        public const string sExLsIsAlternativeTo = "IsAlternativeTo";
        public static Uri ExLsIsMemberOf => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsMemberOf);
        public const string sExLsIsMemberOf = "IsMemberOf";
        public static Uri ExLsIsSpecialisedAs => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsSpecialisedAs);
        public const string sExLsIsSpecialisedAs = "IsSpecialisedAs";
        public static Uri ExLsIsControlledBy => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsControlledBy);
        public const string sExLsIsControlledBy = "IsControlledBy";
        public static Uri ExLsIsSupersededBy => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsIsSupersededBy);
        public const string sExLsIsSupersededBy = "IsSupersededBy";
        public static Uri ExLsHasMember => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsHasMember);
        public const string sExLsHasMember = "HasMember";
        public static Uri ExLsConflictsWith => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_LINKSET_URI.AbsoluteUri, sExLsConflictsWith);
        public const string sExLsConflictsWith = "ConflictsWith";

        #endregion

        #region ExtendedDocument
        public static Uri ExtDocDatabaseLink => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, sExtDocDatabaseLink);
        public const string sExtDocDatabaseLink = "DatabaseLink";


        public static Uri ExtDocPayloadProxy => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, sExtDocPayloadProxy);
        public const string sExtDocPayloadProxy = "PayloadProxy";

        #endregion
        public static Uri OwlOntology => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.OWL_URI.AbsoluteUri, "Ontology");
    }

    public static class IcddConformanceTypesHelper
    {
        public const string Part1 = "ICDD-Part1-Container";
    }
}
