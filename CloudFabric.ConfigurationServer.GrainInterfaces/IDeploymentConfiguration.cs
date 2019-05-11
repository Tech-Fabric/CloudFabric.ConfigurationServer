using System.Threading.Tasks;
using Orleans;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IDeploymentConfiguration : IGrainWithGuidKey, IConfigurationStore
    {
        Task Delete();
    }
}
