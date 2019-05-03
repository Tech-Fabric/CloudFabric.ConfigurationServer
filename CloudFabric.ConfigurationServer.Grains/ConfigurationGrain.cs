using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class ConfigurationGrain : Grain, IConfigurationGrain
    {
        public Task<ConfigurationMetadata> Metadata { get; }

        public Task DeleteConfigurationProperty(ConfigurationProperty property)
        {
            throw new NotImplementedException();
        }

        public Task UpdateConfigurationProperty(ConfigurationProperty property)
        {
            throw new NotImplementedException();
        }
    }
}
