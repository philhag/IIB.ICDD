using System;
using IIB.ICDD.Model.Interfaces;
using IIB.ICDD.Parsing.Vocabulary;
using VDS.RDF;

namespace IIB.ICDD.Model.Container
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  CtPerson 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="CtParty" />

    public class CtPerson : CtParty
    {
        public CtPerson(InformationContainer container, string name, string description) : base(container)
        {
            Description = description;
            Name = name;

        }

        private CtPerson(InformationContainer container, INode resource) : base(container, resource)
        {
        }

        public static CtPerson Read(InformationContainer container, INode resource)
        {
            try
            {
                return new CtPerson(
                    container ?? throw new ArgumentNullException(nameof(container)),
                    resource ?? throw new ArgumentNullException(nameof(resource)));
            }
            catch (ArgumentNullException exception)
            {
                Logging.Logger.Log("Argument Exception: " + exception, Logging.Logger.MsgType.Warning, "CtPerson.Read");
                return null;
            }
        }

        public override string ToString()
        {
            return "Person: " + Name;
        }

        public override Uri ElementType()
        {
            return IcddTypeSpecsHelper.CtPerson;
        }

        public override IUriNode Type()
        {
            return Graph.CreateUriNode(IcddTypeSpecsHelper.CtPerson);
        }
    }
}
