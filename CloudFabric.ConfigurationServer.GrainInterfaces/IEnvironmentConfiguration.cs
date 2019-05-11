using System.Threading.Tasks;
using Orleans;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IEnvironmentConfiguration : IGrainWithGuidKey, IConfigurationStore
    {
        Task Delete();
    }
}
