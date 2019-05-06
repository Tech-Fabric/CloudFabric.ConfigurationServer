using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using Orleans;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IConfiguration : IGrainWithGuidKey
    {
        Task<IClientConfiguration> AddClient(string clientName);
        Task<IClientConfiguration> GetClient(string clientName);
        Task RemoveClient(string clientName);
        Task<string[]> GetAllClientNames();

        Task<ConfigurationProperty[]> GetEffectiveConfiguration(string clientName, string applicationName, string environmentName, string deploymentName = null);
    }
}
