using System;
using System.Collections.Generic;
using System.Linq;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Model.Linkset;
using IIB.ICDD.Model.Linkset.Link;

namespace IIB.ICDD.Validation
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddLogicValidator 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddLogicValidator
    {

        public static List<LsLink> GetConsistentLinks(Dictionary<Type, List<LsLink>> dictTypes, Type typeFilter = null)
        {
            var links = new List<LsLink>();
            var results = dictTypes;
            if (typeFilter != null)
            {
                if (!results.TryGetValue(typeFilter, out links))
                {
                    links = new List<LsLink>();
                }
            }
            else
            {
                var maxCount = -1;
                Type keyMaxItems = null;
                foreach (var pair in results)
                {
                    if (pair.Value.Count <= maxCount)
                        continue;
                    keyMaxItems = pair.Key;
                    maxCount = pair.Value.Count;
                }
                if (keyMaxItems != null)
                {
                    links = results[keyMaxItems];
                }
            }

            return links;
        }
        public static Dictionary<Type, List<LsLink>> PartitionByTypesDic(List<LsLink> values)
        {
            var dictResult = new Dictionary<Type, List<LsLink>>();

            foreach (var value in values)
            {
                if (!dictResult.TryGetValue(value.GetType(), out List<LsLink> listItems))
                {
                    listItems = new List<LsLink>();
                    dictResult[value.GetType()] = listItems;
                }
                listItems.Add(value);
            }
            return dictResult;
        }


    }

    public abstract class IcddLinkValidation
    {
        public abstract Dictionary<string, string> GetKeyValueResults();
    }
    public class IcddLinkConsistencyValidation : IcddLinkValidation
    {
        protected CtLinkset LinkSet;
        public Type ConformLinksetType;
        public List<LsLink> UnconformLinks, ConformLinks;
        public Dictionary<Type, List<LsLink>> DictTypes;
        public IcddLinkConsistencyValidation(CtLinkset linkset, Type typeFilter = null)
        {
            LinkSet = linkset;
            ConformLinksetType = typeFilter;
            DictTypes = IcddLogicValidator.PartitionByTypesDic(linkset.HasLinks);
            ConformLinks = IcddLogicValidator.GetConsistentLinks(DictTypes, ConformLinksetType);
            UnconformLinks = new List<LsLink>();
            UnconformLinks.AddRange(LinkSet.HasLinks);
            foreach (var element in ConformLinks)
            {
                UnconformLinks.Remove(element);
            }
            if (ConformLinks != null && ConformLinks.Count != 0)
            {
                ConformLinksetType = ConformLinks.First().GetType();

            }
            else
            {
                ConformLinksetType = typeof(object);
            }

        }

        public bool IsConform()
        {
            return UnconformLinks.Count == 0;
        }

        public string ConformType()
        {
            return ConformLinksetType.ToString();
        }

        public override Dictionary<string, string> GetKeyValueResults()
        {
            Dictionary<string, string> results = new Dictionary<string, string>
            {
                {"Linkset", LinkSet.FileName},
                {"ValidationRule", "Link Consistency Validation"},
                {"Conformity Type", ConformType()},
                {"Is Conform", IsConform().ToString()},
                {"Unconform Types", UnconformLinks.Count.ToString()}
            };
            return results;
        }
    }

    public class IcddLinkBitotalValidation : IcddLinkValidation
    {
        protected CtLinkset LinkSet;
        public Dictionary<CtDocument, List<LsLinkElement>> RelationSets;
        public Dictionary<CtDocument, List<LsLinkElement>> OverallSets;
        public IcddLinkBitotalValidation(CtLinkset linkset)
        {
            RelationSets = new Dictionary<CtDocument, List<LsLinkElement>>();
            LinkSet = linkset;

            foreach (var link in LinkSet.HasLinks)
            {
                foreach (var linkElement in link.HasLinkElements)
                {
                    if (!RelationSets.ContainsKey(linkElement.HasDocument))
                    {
                        RelationSets.Add(linkElement.HasDocument, new List<LsLinkElement>{ linkElement });
                    }
                    else
                    {
                        RelationSets.TryGetValue(linkElement.HasDocument, out List<LsLinkElement> list);
                        list?.Add(linkElement);
                    }
                }
            }
            OverallSets = linkset.BelongsToContainer.ContainsLinkElement;
        }

        public bool IsBinaryLinkset()
        {
            var cons = new IcddLinkConsistencyValidation(LinkSet, typeof(LsBinaryLink));
            return cons.IsConform();
            //return !(RelationSets.Count > 2);
        }

        public bool IsLeftTotal()
        {
            if (!IsBinaryLinkset())
                return false;

            var bSameItems = true;
            foreach (var elem in OverallSets)
            {
                if (RelationSets.ContainsKey(elem.Key))
                {
                    continue;
                }
                bSameItems = false;
            }
            return bSameItems;

        }
        public bool IsRightTotal()
        {
            if (!IsBinaryLinkset())
                return false;

            var bSameItems = true;
            foreach (var elem in OverallSets)
            {
                if (RelationSets.ContainsKey(elem.Key))
                {
                    continue;
                }
                bSameItems = false;
            }
            return bSameItems;

        }

        public bool IsBiTotal()
        {
            return IsLeftTotal() && IsRightTotal();
        }

        public override Dictionary<string, string> GetKeyValueResults()
        {
            Dictionary<string, string> results = new Dictionary<string, string>
            {
                {"Linkset", LinkSet.FileName},
                {"ValidationRule", "Link Bitotal Validation"},
                {"Is Binary Linkset", IsBinaryLinkset().ToString()},
                {"Is Bitotal Relation", IsBiTotal().ToString()},
                {"Is right-total Relation", IsRightTotal().ToString()},
                {"Is left-total Relation", IsLeftTotal().ToString()}
            };
            return results;
        }
    }

    public class IcddLinkBiuniqueValidation : IcddLinkValidation
    {
        protected CtLinkset LinkSet;
        public Dictionary<CtDocument, List<LsLinkElement>> RelationSets;
        public Dictionary<CtDocument, List<LsLinkElement>> OverallSets;
        public IcddLinkBiuniqueValidation(CtLinkset linkset)
        {
            RelationSets = new Dictionary<CtDocument, List<LsLinkElement>>();
            LinkSet = linkset;

            foreach (var link in LinkSet.HasLinks)
            {
                foreach (var linkElement in link.HasLinkElements)
                {
                    if (!RelationSets.ContainsKey(linkElement.HasDocument))
                    {
                        RelationSets.Add(linkElement.HasDocument, new List<LsLinkElement> { linkElement });
                    }
                    else
                    {
                        RelationSets.TryGetValue(linkElement.HasDocument, out List<LsLinkElement> list);
                        list?.Add(linkElement);
                    }
                }
            }
            OverallSets = linkset.BelongsToContainer.ContainsLinkElement;
        }

        public bool IsBinaryLinkset()
        {
            var conf = new IcddLinkConsistencyValidation(LinkSet, typeof(LsBinaryLink));
            return conf.IsConform();
        }

        public bool IsLeftUnique()
        {
            if (!IsBinaryLinkset())
                return false;
            var isUnique = true;
            foreach (var fromLink in LinkSet.HasLinks)
            {
                if (!(fromLink is LsBinaryLink l)) continue;
                var sElem = l.First;


                var first = false;
                foreach (var toLink in LinkSet.HasLinks)
                {
                    if (!first && toLink.HasLinkElements.Contains(sElem))
                    {
                        first = true;

                    }
                    if (first && toLink.HasLinkElements.Contains(sElem))
                    {
                        isUnique = false;
                    }
                }
            }
            return isUnique;

        }
        public bool IsRightUnique()
        {
            if (!IsBinaryLinkset())
                return false;
            var isUnique = true;
            foreach (var fromLink in LinkSet.HasLinks)
            {
                if (!(fromLink is LsBinaryLink l)) continue;
                var sElem = l.Second;


                var first = false;
                foreach (var toLink in LinkSet.HasLinks)
                {
                    if (!first && toLink.HasLinkElements.Contains(sElem))
                    {
                        first = true;

                    }
                    if (first && toLink.HasLinkElements.Contains(sElem))
                    {
                        isUnique = false;
                    }
                }
            }
            return isUnique;

        }

        public bool IsBiunique()
        {
            return IsLeftUnique() && IsRightUnique();
        }

        public override Dictionary<string, string> GetKeyValueResults()
        {
            Dictionary<string, string> results = new Dictionary<string, string>
            {
                {"Linkset", LinkSet.FileName},
                {"ValidationRule", "Link Biunique Validation"},
                {"Is Binary Linkset", IsBinaryLinkset().ToString()},
                {"Is Biunique Relation", IsBiunique().ToString()},
                {"Is right-unique Relation", IsRightUnique().ToString()},
                {"Is left-unique Relation", IsLeftUnique().ToString()}
            };
            return results;
        }
    }
}

