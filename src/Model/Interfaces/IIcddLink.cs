using IIB.ICDD.Model.Container.Document;
using VDS.RDF;

namespace IIB.ICDD.Model.Interfaces
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Interface:  IIcddLink 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal interface IIcddLink
    {
        bool RefersToDocument(CtDocument document);

        IGraph AsSubgraph();

    }
}
