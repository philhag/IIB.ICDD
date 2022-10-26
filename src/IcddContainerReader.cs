using System.Collections.Generic;
using System.IO;
using IIB.ICDD.Handling;
using IIB.ICDD.Model;
using IIB.ICDD.Validation;

namespace IIB.ICDD
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddContainerReader 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddContainerReader
    {
        protected string File;
        protected string Workfolder;
        protected IcddContainerReaderOptions Options;
        protected IcddValidator Validator;

        /// <summary>
        /// Initializes a container reader from a filepath, optionally accepts IcddContainerReaderOptions
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="options"></param>
        /// <exception cref="IcddException"></exception>
        public IcddContainerReader(string filepath, IcddContainerReaderOptions options = null)
        {
            if (options == null)
            {
                if (System.IO.File.Exists(filepath))
                {
                    File = filepath;
                    Validator = new IcddValidator(File);
                }
                else
                {
                    throw new IcddException("File not found.", new FileNotFoundException());
                }
            }
            else
            {
                Options = options;
                if (options.UseCustomWorkfolder || options.UseCustomGuid)
                {
                    Workfolder = options.CustomWorkfolder;
                    var container = new InformationContainer(filepath, options.UseCustomWorkfolder, options.CustomWorkfolder, options.UseCustomGuid, options.CustomGuid);
                    Validator = new IcddValidator(container);
                }
                else
                {
                    if (System.IO.File.Exists(filepath))
                    {
                        File = filepath;
                        Validator = new IcddValidator(File);
                    }
                    else
                    {
                        throw new IcddException("File not found.", new FileNotFoundException());
                    }
                }
            }
        }

        /// <summary>
        /// Reads and validates the container
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IcddException"></exception>
        public InformationContainer Read()
        {
            Validator?.Validate();
            if (IsValid())
            {
                return Validator.GetValidContainer();
            }
            throw new IcddException("Container is invalid.");
        }

        /// <summary>
        /// Gets the validation results from reading the container
        /// </summary>
        /// <returns></returns>
        public List<IcddValidationResult> GetValidationResults()
        {
            return Validator?.GetResults();
        }

        /// <summary>
        /// Returns whether the container is valid or not
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return Validator != null && Validator.IsValid();
        }

    }

    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddContainerReaderOptions 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddContainerReaderOptions
    {
        public string CustomWorkfolder;
        public bool UseCustomWorkfolder;

        public string CustomGuid;
        public bool UseCustomGuid;

        /// <summary>
        /// Specifies container reader options wih a workfolder path
        /// </summary>
        /// <param name="workfolder"></param>
        public IcddContainerReaderOptions(string workfolder)
        {
            UseCustomWorkfolder = true;
            CustomWorkfolder = workfolder;
        }
        /// <summary>
        /// Specifies container reader options wih a workfolder path and a predefined container guid that shall be applied to the container
        /// </summary>
        /// <param name="workfolder"></param>
        /// <param name="guid"></param>
        public IcddContainerReaderOptions(string workfolder, string guid)
        {
            UseCustomWorkfolder = true;
            CustomWorkfolder = workfolder;

            UseCustomGuid = true;
            CustomGuid = guid;
        }

    }

}