using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IApplicationEnvironmentConfiguration : IConfigurationStore
    {
        Task<IDeploymentConfiguration> AddDeployment(string deploymentName);
        Task<IDeploymentConfiguration> GetDeployment(string deploymentName);
        Task RemoveDeployment(string deploymentName);
        Task<string[]> GetAllDeploymentNames();

        Task<ConfigurationProperty[]> GetEffectiveConfiguration(string deploymentName = null);
    }
}
