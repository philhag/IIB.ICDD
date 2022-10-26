using VDS.RDF;

namespace IIB.ICDD.Conversion
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IConverter 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    interface IConverter
    {
        Graph ConvertToGraph();
        string ConvertToFile();
    }
}
