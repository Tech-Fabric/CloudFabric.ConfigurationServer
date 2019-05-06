using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IConfigurationStore
    {
        Task SetProperty(string propertyName, string value);
        Task RemoveProperty(string propertyName);
        Task<ConfigurationProperty> GetProperty(string propertyName);
        Task<ConfigurationProperty> GetAllProperies();
    }
}
