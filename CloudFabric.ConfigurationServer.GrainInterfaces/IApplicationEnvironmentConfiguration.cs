using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using Orleans;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IApplicationEnvironmentConfiguration : IGrainWithGuidKey, IConfigurationStore
    {
        Task<IDeploymentConfiguration> AddDeployment(string deploymentName);
        Task<IDeploymentConfiguration> GetDeployment(string deploymentName);
        Task RemoveDeployment(string deploymentName);
        Task<string[]> GetAllDeploymentNames();

        Task<ConfigurationProperty[]> GetEffectiveConfiguration(string deploymentName = null);

        Task Delete();
    }
}
