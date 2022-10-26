using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace IIB.ICDD.Logging
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  Logger 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class Logger
    {
        public enum MsgType
        {
            Error,
            Info,
            Warning
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Logger();
                    }
                }
                return _instance;
            }
        }

        private static Logger _instance;
        private static readonly object SyncRoot = new();
        private static readonly ReaderWriterLockSlim ReadWriteLock = new();

        private Logger()
        {
            LogFileName = DateTime.Now.ToString("dd-MM-yyyy");
            LogFileExtension = ".xml";
            LogPath = "ProgramLog/";
        }

        public StreamWriter Writer { get; set; }

        public string LogPath { get; set; }

        public string LogFileName { get; set; }

        public string LogFileExtension { get; set; }

        public string LogFile => LogFileName + LogFileExtension;

        public string LogFullPath => Path.Combine(LogPath, LogFile);

        public bool LogExists => File.Exists(LogFullPath);

        public void WriteToLog(string inLogMessage, MsgType msgtype, string functionCall)
        {
            ReadWriteLock.EnterWriteLock();
            try
            {
                LogFileName = DateTime.Now.ToString("dd-MM-yyyy");

                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }

                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Indent = true
                };

                var sbuilder = new StringBuilder();
                using (var sw = new StringWriter(sbuilder))
                {
                    using (var w = XmlWriter.Create(sw, settings))
                    {
                        w.WriteStartElement("LogEntry");

                        switch (msgtype)
                        {
                            case MsgType.Error:
                                w.WriteAttributeString("Type", "Error");
                                break;
                            case MsgType.Info:
                                w.WriteAttributeString("Type", "Info");
                                break;
                            case MsgType.Warning:
                                w.WriteAttributeString("Type", "Warning");
                                break;
                        }

                        w.WriteElementString("Time", DateTime.Now.ToString(CultureInfo.CurrentCulture));
                        w.WriteElementString("Message", inLogMessage);
                        w.WriteElementString("Function", functionCall);
                        w.WriteEndElement();
                    }
                }
                using (var writer = new StreamWriter(LogFullPath, true, Encoding.UTF8))
                {
                    writer.WriteLine(sbuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                ReadWriteLock.ExitWriteLock();
            }
        }

        public static void Log(String inLogMessage, MsgType msgtype, string functionCall)
        {
            Instance.WriteToLog(inLogMessage, msgtype, functionCall);
        }
    }
}
