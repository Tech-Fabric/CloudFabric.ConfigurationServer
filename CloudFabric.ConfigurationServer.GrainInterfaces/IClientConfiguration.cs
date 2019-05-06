using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IClientConfiguration : IConfigurationStore
    {
        Task<IEnvironmentConfiguration> AddEnvironment(string environmentName);
        Task<IEnvironmentConfiguration> GetEnvironment(string environmentName);
        Task RemoveEnvironment(string environmentName);
        Task<string[]> GetAllEnvironmentNames();

        Task<IApplicationConfiguration> AddApplication(string applicationName);
        Task<IApplicationConfiguration> GetApplication(string applicationName);
        Task RemoveApplication(string applicationName);
        Task<string[]> GetAllApplicationNames();

        Task<ConfigurationProperty[]> GetEffectiveConfiguration(string applicationName, string environmentName, string deploymentName = null);
    }
}
