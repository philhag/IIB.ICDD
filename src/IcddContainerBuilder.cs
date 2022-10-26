using System.Threading.Tasks;
using IIB.ICDD.Model;
using IIB.ICDD.Model.Container;

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
        internal InformationContainer Container;
        internal readonly IcddContainerBuilderOptions Options;

        /// <summary>
        /// Initializes a container builder, optionally accepts IcddContainerBuilderOptions
        /// </summary>
        /// <param name="options"></param>
        public IcddContainerBuilder(IcddContainerBuilderOptions options = null)
        {
            Options = options;
        }

        /// <summary>
        /// Resets the builder to generate a new container instance
        /// </summary>
        public void Reset()
        {
            Container = Options == null
                ? new InformationContainer()
                : new InformationContainer(Options);
        }

        /// <summary>
        /// Returns a new empty container
        /// </summary>
        /// <returns></returns>
        public InformationContainer GetEmptyContainer()
        {
            Reset();
            return Container;
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
                return Container;
            });
        }

        /// <summary>
        /// Returns a new assebled container
        /// </summary>
        /// <returns></returns>
        public InformationContainer GetAssembledContainer()
        {
            Reset();
            InformationContainer result = Container;
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
                InformationContainer result = Container;
                result.ContainerDescription = new CtContainerDescription(result,
                    "Initial version of " + result.ContainerName, "1", "Initial Version");
                return result;
            });

        }
    }
}

