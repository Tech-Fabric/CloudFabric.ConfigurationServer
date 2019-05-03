using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public interface IConfigurationGrain
    {
        Task<ConfigurationMetadata> Metadata { get; }
        Task UpdateConfigurationProperty(ConfigurationProperty property);
        Task DeleteConfigurationProperty(ConfigurationProperty property);
    }
}
