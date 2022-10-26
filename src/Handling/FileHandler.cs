using System;
using System.IO;
using System.Linq;

namespace IIB.ICDD.Handling
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  FileHandler 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    internal class FileHandler
    {
        internal static bool FileExists(string destinationFilePath, out string newDestinationFilePath, out string newFilename)
        {
            var fileExists = File.Exists(destinationFilePath);
            if (!fileExists)
            {
                newDestinationFilePath = destinationFilePath;
                newFilename = Path.GetFileName(newDestinationFilePath);
            }
            else
            {
                newDestinationFilePath = MakeUnique(destinationFilePath).FullName;
                newFilename = Path.GetFileName(newDestinationFilePath);
            }
            return fileExists;
        }

        internal static FileInfo MakeUnique(string path)
        {
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileExt = Path.GetExtension(path);

            for (int i = 1; ; ++i)
            {
                if (!File.Exists(path))
                    return new FileInfo(path);

                path = Path.Combine(dir, fileName + "(" + i + ")" + fileExt);
            }
        }
        internal static string GetWorkFolder()
        {
            var workFolder = Path.Combine(Path.GetTempPath(), "icdd");
#if DEBUG
           // workFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "icdd");
#endif

            Directory.CreateDirectory(workFolder);
            return workFolder;
        }

        internal static string GetLoggingFolder()
        {
            var logPath = Path.Combine(GetWorkFolder(), "logs");
            Directory.CreateDirectory(logPath);
            return logPath;
        }

        internal static string GetContainerPath(string guid, string alternativeWorkfolder = "", bool create = true)
        {
            if (!string.IsNullOrEmpty(alternativeWorkfolder))
            {
                if (!Directory.Exists(alternativeWorkfolder) && create)
                {
                    Directory.CreateDirectory(alternativeWorkfolder);
                }

                if (Directory.Exists(alternativeWorkfolder))
                {
                    string containerPathAlt = Path.Combine(alternativeWorkfolder, guid);
                    if (!Directory.Exists(containerPathAlt) && create)
                    {
                        Directory.CreateDirectory(containerPathAlt);
                    }
                    return containerPathAlt;
                }
            }
            string containerPath = Path.Combine(GetWorkFolder(), "containers", guid);
            if (create)
            {
                Directory.CreateDirectory(containerPath);
            }
            return containerPath;
        }

        internal static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new(sourceDirectory);
            DirectoryInfo diTarget = new(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(path)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        public static bool PathEquals(string path1, string path2)
        {
            FileInfo f1 = new(NormalizePath(path1));
            FileInfo f2 = new(NormalizePath(path2));

            return f1.FullName == f2.FullName;
        }

        public static bool IsRdf(string filepath)
        {
            var exists = File.Exists(filepath);
            var ext = Path.GetExtension(filepath).ToLower();
            return exists && (ext == ".rdf" || ext == ".ttl");
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                if (diSourceSubDir.Name != ".git")
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    CopyAll(diSourceSubDir, nextTargetSubDir);
                }
            }
        }

        public static void DeleteReadOnlyDirectory(string directory)
        {
            foreach (var subdirectory in Directory.EnumerateDirectories(directory))
            {
                DeleteReadOnlyDirectory(subdirectory);
            }
            foreach (var fileName in Directory.EnumerateFiles(directory))
            {
                var fileInfo = new FileInfo(fileName)
                {
                    Attributes = FileAttributes.Normal
                };
                fileInfo.Delete();
            }
            Directory.Delete(directory);
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, string[] excludeSubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    if (excludeSubDirs.Contains(subdir.Name))
                        continue;
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs, excludeSubDirs);
                }
            }
        }

    }
}
