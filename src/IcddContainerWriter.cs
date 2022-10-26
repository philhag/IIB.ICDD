using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Validation;

namespace IIB.ICDD
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddContainerWriter 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddContainerWriter
    {
        internal string FilePath;
        internal bool Validate;
        internal IcddValidator Validator;
        internal static readonly ReaderWriterLockSlim ReadWriteLock = new();

        /// <summary>
        /// Initializes a new writer instance with a specified output path
        /// </summary>
        /// <param name="file"></param>
        /// <param name="validate"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public IcddContainerWriter(string file, bool validate = false)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException();

            Validate = validate;
            FilePath = file;
        }


        /// <summary>
        /// Writes a container using the configuration of the IcddContainerWriter instance
        /// </summary>
        /// <param name="container"></param>
        /// <exception cref="IcddException"></exception>
        public void Write(InformationContainer container)
        {
            ReadWriteLock.TryEnterWriteLock(10000);
            try
            {
                if (Validate)
                {
                    Validator = new IcddValidator(container);
                    Validator.Validate();
                    container = Validator.GetValidContainer();

                    if (container == null)
                    {
                        throw new IcddException("The operation is not valid due to the current validation state of the container.");
                    }
                }
                CompressContainer(container);
            }
            catch (IcddException e)
            {
                Logger.Log(e.Message, Logger.MsgType.Error, GetType().ToString());
                throw;
            }
            finally
            {
                ReadWriteLock.ExitWriteLock();
            }

        }

        private void CompressContainer(InformationContainer container)
        {
            if (container != null)
            {
                container.SaveRdf();
                foreach (var linkset in container.ContainerDescription.ContainsLinkset)
                {
                    linkset.SaveRdf();
                }

                var compressedContainer = Path.Combine(FileHandler.GetWorkFolder(), container.ContainerName.Split('.').FirstOrDefault() + "_" + container.ContainerGuid + ".zip");

                if (string.IsNullOrEmpty(FilePath)) throw new ArgumentNullException();

                if (File.Exists(compressedContainer) || File.Exists(FilePath))
                {
                    File.Delete(compressedContainer);
                    File.Delete(FilePath);
                }

                var temp = Path.Combine(FileHandler.GetWorkFolder(), container.ContainerName.Split('.').FirstOrDefault() + "_" + container.ContainerGuid + "_temp");
                FileHandler.DirectoryCopy(container.PathToContainer, temp, true, new[] { ".git" });

                ZipFile.CreateFromDirectory(temp, compressedContainer);
                File.Copy(compressedContainer, FilePath);
                File.Delete(compressedContainer);
                Directory.Delete(temp, true);
            }
            else
            {
                throw new InvalidOperationException("The operation is not valid due to the current validation state of the container.");
            }

        }
    }

}
