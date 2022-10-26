using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Nodes;

namespace IIB.ICDD.Handling
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddNodeHandler 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal static class IcddNodeHandler
    {
        internal static string SeparateFragment(INode node)
        {
            if (node is not { NodeType: NodeType.Uri })
                return "";

            var uriFragment = ((UriNode)node).Uri.Fragment;
            return uriFragment.TrimStart('#');
        }

        internal static Uri CreateUriWithFragment(string ns, string fragment)
        {
            if (ns == null)
                return null;

            if (fragment == null)
                return new Uri(ns.TrimEnd('#'));

            return new Uri(ns.TrimEnd('#') + "#" + fragment.TrimStart('#'));
        }
        

        public static Uri GetNodeUri(this INode node, Func<Exception> getError = null)
        {
            IUriNode uriNode = node as IUriNode;
            if (uriNode != null)
            {
                return uriNode.Uri;
            }

            if (node != null && getError != null)
            {
                throw getError();
            }

            return null;
        }

        public static string GeNodeLiteral(this INode node, Func<Exception> getError = null)
        {
            ILiteralNode literalNode = node as ILiteralNode;
            if (literalNode != null)
            {
                return literalNode.Value;
            }

            if (node != null && getError != null)
            {
                throw getError();
            }

            return null;
        }
    }


}
