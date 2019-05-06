using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class Configuration : Grain, IConfiguration
    {
        public Task<IClientConfiguration> AddClient(string clientName)
        {
            throw new System.NotImplementedException();
        }

        public Task<string[]> GetAllClientNames()
        {
            throw new System.NotImplementedException();
        }

        public Task<IClientConfiguration> GetClient(string clientName)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConfigurationProperty[]> GetEffectiveConfiguration(string clientName, string applicationName, string environmentName, string deploymentName = null)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveClient(string clientName)
        {
            throw new System.NotImplementedException();
        }
    }
}
