using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using Orleans;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IApplicationConfiguration : IGrainWithGuidKey, IConfigurationStore
    {
        Task<IApplicationEnvironmentConfiguration> AddEnvironment(string environmentName);
        Task<IApplicationEnvironmentConfiguration> GetEnvironment(string environmentName);
        Task RemoveEnvironment(string environmentName);
        Task<string[]> GetAllEnvironmentNames();

        Task<ConfigurationProperty[]> GetEffectiveConfiguration(string environmentName, string deploymentName = null);
    }
}
