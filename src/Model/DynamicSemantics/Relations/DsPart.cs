using VDS.RDF;

namespace IIB.ICDD.Model.DynamicSemantics.Relations
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  DsPart 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    /// <seealso cref="DsEntity" />
    public class DsPart : DsEntity
    {

        protected DsPart(Graph graph, InformationContainer container, INode resource) : base(graph, container, resource)
        {
        }
    }
}
