using System;
using IIB.ICDD.Logging;

namespace IIB.ICDD.Handling
{

    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddException 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddException : Exception
    {
        public IcddException()
        {
        }

        public IcddException(string message) : base(message)
        {
            Logger.Log(message, Logger.MsgType.Error, "IcddException");
        }

        public IcddException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.Log(message + ": " + innerException, Logger.MsgType.Error, innerException.Source);
        }
    }
}
