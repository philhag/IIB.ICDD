using System;
using IIB.ICDD.Handling;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Interfaces;
using VDS.RDF;

namespace IIB.ICDD.Parsing.Vocabulary
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddPredicateSpecsHelper 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public static class IcddPredicateSpecsHelper
    {
        public static Uri NAME => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "name");
        public static Uri DESCRIPTION => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "description");
        public static Uri CONFORMANCE_INDICATOR => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "conformanceIndicator");
        public static Uri CONTAINS_DOCUMENT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "containsDocument");
        public static Uri CONTAINS_LINKSET => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "containsLinkset");
        public static Uri BELONGS_TO_CONTAINER => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "belongsToContainer");

        #region Documents
        public static Uri URL => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "url");
        public static Uri IS_REQUESTED => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "requested");
        public static Uri FILE_FORMAT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "format");
        public static Uri FILE_TYPE => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "filetype");
        public static Uri FOLDER_NAME => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "foldername");
        public static Uri CHECKSUM => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "checksum");
        public static Uri CHECKSUM_ALGORITHM => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "checksumAlgorithm");
        public static Uri ENCRYPTION_ALGORITHM => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "encryptionAlgorithm");
        public static Uri FILE_NAME => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "filename");
        public static Uri ALTERNATIVE_DOCUMENT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "alternativeDocument");
        #endregion

        #region ExtendedDocument
        public static Uri ExtDbConnectionString => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, "databaseConnectionString");
        public static Uri ExtDbName => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, "databaseName");
        public static Uri ExtDbQueryLanguage => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, "databaseQueryLanguage");
        public static Uri ExtDbType => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, "databaseType");
        public static Uri ExtDbMapping => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, "databaseMapping");
        public static Uri ExtBaseUri => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, "baseUri");
        public static Uri ExtFileName => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.EXT_DOCUMENT_URI.AbsoluteUri, "fileName");
        #endregion

        #region Linkset
        public static Uri QUERY_EXPRESSION => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "queryExpression");
        public static Uri QUERY_LANGUAGE => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "queryLanguage");
        public static Uri URI => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "uri");
        public static Uri IDENTIFIER => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "identifier");
        public static Uri IDENTIFIER_FIELD => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "identifierField");
        public static Uri HAS_IDENTIFIER => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "hasIdentifier");
        public static Uri HAS_LINK_ELEMENT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "hasLinkElement");
        public static Uri HAS_FROM_LINK_ELEMENT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "hasFromLinkElement");
        public static Uri HAS_TO_LINK_ELEMENT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "hasToLinkElement");
        public static Uri HAS_DOCUMENT => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.LINKSET_URI.AbsoluteUri, "hasDocument");

        #endregion

        #region Versioning
        public static Uri MODIFICATION_DATE => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "modificationDate");
        public static Uri CREATION_DATE => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "creationDate");
        public static Uri IS_MODIFIED => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "isModified");

        [Obsolete("This property is obsolete. Use MODIFIED_BY instead.", true)]
        public static Uri MODIFIER => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "modifier");

        [Obsolete("This property is obsolete. Use CREATED_BY instead.", true)]
        public static Uri CREATOR => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "creator");
        public static Uri VERSION_DESCRIPTION => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "versionDescription");
        public static Uri VERSION_ID => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "versionId");
        public static Uri PUBLISHED_BY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "publishedBy");
        public static Uri CREATED_BY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "createdBy");
        public static Uri MODIFIED_BY => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "modifiedBy");
        public static Uri PRIOR_VERSION => IcddNodeHandler.CreateUriWithFragment(IcddNamespacesHelper.CONTAINER_URI.AbsoluteUri, "priorVersion");

        #endregion
    }
}
