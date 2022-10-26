using System;
using System.Diagnostics;
using System.IO;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model.Container.Document;
using IIB.ICDD.Parsing;
using Microsoft.Win32;
using VDS.RDF;

namespace IIB.ICDD.Conversion.Ifc
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IfcConverter 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IfcConverter : IConverter
    {
        private CtInternalDocument _document;
        private readonly string _workFolder =
            Path.Combine(FileHandler.GetWorkFolder(),
                "Converter", "Ifc",
                DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                DateTime.Now.ToShortTimeString().Replace(":", "-") + "-" + DateTime.Now.Second);


        public IfcConverter(CtInternalDocument document)
        {
            _document = document;
        }
        private static string GetJavaInstallationPath()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            try
            {


                string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
                if (!Environment.Is64BitOperatingSystem)
                {
                    using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey))
                    {
                        string currentVersion = rk.GetValue("CurrentVersion").ToString();
                        using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
                        {
                            return key.GetValue("JavaHome").ToString();
                        }
                    }
                }
                else
                {
                    using var view64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                                                                RegistryView.Registry64);
                    using var clsid64 = view64.OpenSubKey(javaKey);
                    string currentVersion = clsid64.GetValue("CurrentVersion").ToString();
                    using RegistryKey key = clsid64.OpenSubKey(currentVersion);
                    return key.GetValue("JavaHome").ToString();
                }
            }
            catch (Exception e)
            {
                throw new IcddException("Java not installed.", e);
            }

        }

        public CtInternalDocument ConvertToContainerDocument()
        {
            var path = ConvertToFile();
            var document = _document.Container.CreateInternalDocument(path, Path.GetFileName(path), "ttl", "text/turtle");

            return document;
        }

        public string ConvertToFile()
        {
            if (string.IsNullOrEmpty(GetJavaInstallationPath()))
            {
                Logger.Log("Keine Java Installation gefunden. Ausführung nicht möglich.", Logger.MsgType.Error, "IfcConverter.ConvertToGraph");
                return null;
            }
            string sourceFile = _document.AbsoluteFilePath();
            string javaExePath = Path.Combine(GetJavaInstallationPath(), "bin\\Java.exe");
            string inputFile = Path.Combine(_workFolder, _document.Name);
            string outputFile = Path.ChangeExtension(Path.Combine(_workFolder, "converted_" + _document.Name), "ttl");

            string converterPath = Path.Combine(Directory.GetCurrentDirectory(), "Conversion", "IFC", "ifc2owl.jar");

            if (!Directory.Exists(_workFolder))
            {
                Directory.CreateDirectory(_workFolder);
            }

            if (File.Exists(sourceFile)
                && File.Exists(javaExePath)
                && File.Exists(converterPath))
            {
                File.Copy(sourceFile, inputFile);
                File.Copy(converterPath, Path.Combine(_workFolder, "ifc2owl.jar"));
                var input = _document.Name;
                var output = "converted_" + Path.ChangeExtension(_document.Name, "ttl");
                var startInfo = new ProcessStartInfo(javaExePath,
                    "-jar ifc2owl.jar " + input + " " + output + " -m")
                {
                    UseShellExecute = false,
                    WorkingDirectory = _workFolder,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false
                };
                //startInfo.UserName = "Administrator";
                var process = Process.Start(startInfo);
                var outputText = process?.StandardOutput.ReadToEnd();
                Logger.Log(outputText, Logger.MsgType.Info, "IfcConverter.ConvertToGraph");

            }
            File.Delete(inputFile);
            File.Delete(Path.Combine(_workFolder, "ifc2owl.jar"));
            Logger.Log("Saved converted file to: " + outputFile, Logger.MsgType.Info, "IfcConverter.ConvertToGraph");
            return !File.Exists(outputFile) ? "" : outputFile;
        }

        public Graph ConvertToGraph()
        {
            if (string.IsNullOrEmpty(GetJavaInstallationPath()))
            {
                Logger.Log("Keine Java Installation gefunden. Ausführung nicht möglich.", Logger.MsgType.Error, "IfcConverter.ConvertToGraph");
                return null;
            }
            string sourceFile = _document.AbsoluteFilePath();
            string javaExePath = Path.Combine(GetJavaInstallationPath(), "bin\\Java.exe");
            string inputFile = Path.Combine(_workFolder, _document.Name);
            string outputFile = Path.ChangeExtension(Path.Combine(_workFolder, "converted_" + _document.Name), "ttl");

            string converterPath = Path.Combine(Directory.GetCurrentDirectory(), "Conversion", "IFC", "ifc2owl.jar");

            if (!Directory.Exists(_workFolder))
            {
                Directory.CreateDirectory(_workFolder);
            }

            if (File.Exists(sourceFile)
                && File.Exists(javaExePath)
                && File.Exists(converterPath))
            {
                File.Copy(sourceFile, inputFile);
                File.Copy(converterPath, Path.Combine(_workFolder, "ifc2owl.jar"));
                var input = _document.Name;
                var output = "converted_" + Path.ChangeExtension(_document.Name, "ttl");
                var startInfo = new ProcessStartInfo(javaExePath,
                    "-jar ifc2owl.jar " + input + " " + output + " -m")
                {
                    UseShellExecute = false,
                    WorkingDirectory = _workFolder,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false
                };
                //startInfo.UserName = "Administrator";
                var process = Process.Start(startInfo);
                var outputText = process?.StandardOutput.ReadToEnd();
                Logger.Log(outputText, Logger.MsgType.Info, "IfcConverter.ConvertToGraph");

            }
            if (File.Exists(outputFile))
            {
                var result = TurtleReader.Read(outputFile);
                File.Delete(inputFile);
                File.Delete(outputFile);
                File.Delete(Path.Combine(_workFolder, "ifc2owl.jar"));
                Directory.Delete(_workFolder);
                return result;
            }

            File.Delete(inputFile);
            Directory.Delete(_workFolder);
            return null;
        }
    }
}
