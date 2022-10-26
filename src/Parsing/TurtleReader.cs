using System;
using IIB.ICDD.Logging;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Shacl;

namespace IIB.ICDD.Parsing
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  TurtleReader 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public static class TurtleReader
    {

        /// <summary>
        /// Reads the specified turtle file.
        /// </summary>
        /// <param name="turtleFile">The turtle file.</param>
        /// <returns></returns>
        public static Graph Read(string turtleFile)
        {
            Graph g = new();
            g.LoadFromFile(turtleFile, new TurtleParser());
            return g;
        }


        /// <summary>
        /// Reads the shapes.
        /// </summary>
        /// <param name="turtleFile">The turtle file.</param>
        /// <returns></returns>
        public static ShapesGraph ReadShapes(string turtleFile)
        {
            Graph g = Read(turtleFile);
            return new ShapesGraph(g);
        }

        /// <summary>
        /// Reads the shapes.
        /// </summary>
        /// <param name="turtleFile">The turtle file.</param>
        /// <returns></returns>
        public static ShapesGraph ReadShapes(Uri turtleFile)
        {
            try
            {
                Graph g = new();
                g.LoadFromUri(turtleFile, new TurtleParser());
                return new ShapesGraph(g);
            }
            catch
            {
                Logger.Log("Error while reading shapes from " + turtleFile, Logger.MsgType.Error,
                    " ReadShapes()");
            }
            return new ShapesGraph(new Graph());
        }

        /// <summary>
        /// Reads the shapes.
        /// </summary>
        /// <param name="turtleFile">The turtle file.</param>
        /// <returns></returns>
        public static Graph ReadData(string turtleFile)
        {
            return Read(turtleFile);
        }
    }

}
