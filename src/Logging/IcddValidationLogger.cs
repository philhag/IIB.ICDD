using System;
using System.IO;
using System.Threading;

namespace IIB.ICDD.Logging
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddValidationLogger 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddValidationLogger
    {
        protected static StreamWriter LogStreamWriter;
        private static readonly ReaderWriterLockSlim ReadWriteLock = new();
        public static void Initialize(string logPath)
        {
            if (!File.Exists(logPath))
            {
                var x = File.Create(logPath);
                x.Close();
            }
            //ReadWriteLock.EnterWriteLock();
            LogStreamWriter = new StreamWriter(logPath);
            Log("ICDDToolkitLibrary (c) 2021 by Philipp Hagedorn ");
            Log("for further information contact philipp.hagedorn-n6v@rub.de");
            Log("-------------------------------------------------------");
        }

        public static void Log(string logText)
        {
            LogStreamWriter.WriteLine(DateTime.Now.ToLongTimeString() + ": " + logText);
        }

        public static void Save()
        {
            LogStreamWriter.Close();
            //ReadWriteLock.ExitWriteLock();
        }
    }
}
