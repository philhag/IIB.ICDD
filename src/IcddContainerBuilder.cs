using System.Threading.Tasks;
using IIB.ICDD.Handling;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container;
using IIB.ICDD.Parsing.Vocabulary;

namespace IIB.ICDD
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddContainerBuilder 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddContainerBuilder
    {
        private InformationContainer _container;
        private readonly IcddContainerBuilderOptions _options;

        /// <summary>
        /// Initializes a container builder, optionally accepts IcddContainerBuilderOptions
        /// </summary>
        /// <param name="options"></param>
        public IcddContainerBuilder(IcddContainerBuilderOptions options = null)
        {
            _options = options;
        }

        /// <summary>
        /// Resets the builder to generate a new container instance
        /// </summary>
        public void Reset()
        {
            _container = _options == null
                ? new InformationContainer()
                : new InformationContainer(_options);
        }

        /// <summary>
        /// Returns a new empty container
        /// </summary>
        /// <returns></returns>
        public InformationContainer GetEmptyContainer()
        {
            Reset();
            return _container;
        }

        /// <summary>
        /// Returns a new empty container as an async task
        /// </summary>
        /// <returns></returns>
        public async Task<InformationContainer> GetEmptyContainerAsync()
        {
            return await Task.Run(() =>
            {
                Reset();
                return _container;
            });
        }

        /// <summary>
        /// Returns a new assebled container
        /// </summary>
        /// <returns></returns>
        public InformationContainer GetAssembledContainer()
        {
            Reset();
            InformationContainer result = _container;
            result.ContainerDescription = new CtContainerDescription(result,
                "Initial version of " + result.ContainerName, "1", "Initial Version");
            return result;
        }
        /// <summary>
        /// Returns a new assembled container as an async task
        /// </summary>
        /// <returns></returns>
        public async Task<InformationContainer> GetAssembledContainerAsync()
        {
            return await Task.Run(() =>
            {
                Reset();
                InformationContainer result = _container;
                result.ContainerDescription = new CtContainerDescription(result,
                    "Initial version of " + result.ContainerName, "1", "Initial Version");
                return result;
            });

        }
    }


    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddContainerBuilderOptions 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddContainerBuilderOptions
    {
        /// <summary>
        /// Gets or sets a custom workfolder
        /// </summary>
        public string CustomWorkfolder { get; set; }
        /// <summary>
        /// Gets or sets a custom guid
        /// </summary>
        public string CustomGuid { get; set; }
        /// <summary>
        /// Gets or sets the container file name
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets the container namespace
        /// </summary>
        public string Namespace => string.IsNullOrEmpty(ContainerName) ? "" : IcddNamespacesHelper.BaseNamespaceFor(ContainerName, "index", "");
        /// <summary>
        /// Returns whether custom workfolder shall be used or not
        /// </summary>
        public bool UseCustomWorkfolder => string.IsNullOrEmpty(CustomWorkfolder);
        /// <summary>
        /// Returns whether custom guid shall be used or not
        /// </summary>
        public bool UseCustomGuid => string.IsNullOrEmpty(CustomWorkfolder);
    }
}

