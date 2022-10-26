using System;
using System.Collections.Generic;
using IIB.ICDD.Parsing;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtParty 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="IcddBaseElement" />
    [Serializable]
    public abstract class CtParty : IcddBaseElement
    {

        protected CtParty(InformationContainer container, INode resource) : base(container.IndexGraph, container, resource)
        {
        }

        protected CtParty(InformationContainer container) : base(container.IndexGraph, container)
        {
        }

        public string Name
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.NAME);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.NAME, value, AttributeType.String);

        }

        public string Description
        {
            get => RdfReader.GetDataPropertyAsString(Graph, this, IcddPredicateSpecsHelper.DESCRIPTION);
            set => RdfWriter.SetDataProperty(Graph, this, IcddPredicateSpecsHelper.DESCRIPTION, value, AttributeType.String);

        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return base.Equals(obj);
            }
            else
            {
                CtParty p = (CtParty)obj;
                return base.Equals(obj)
                    && (Name == p.Name)
                    && (Description == p.Description);
            }
        }

        public override int GetHashCode()
        {
            int hashCode = -1598788603;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }
    }

}