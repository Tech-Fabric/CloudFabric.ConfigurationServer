using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class DeploymentConfiguration : Grain<DeploymentConfiguration.DeploymentConfigurationState>, IDeploymentConfiguration
    {
        public Task Delete() => this.ClearStateAsync();

        public Task<ConfigurationProperty[]> GetAllProperies() => Task.FromResult(this.State.Properties.Select(x => new ConfigurationProperty(x.Key, x.Value)).ToArray());

        public Task<ConfigurationProperty> GetProperty(string propertyName)
        {
            if (!this.State.Properties.ContainsKey(propertyName))
                throw new Exception();

            return Task.FromResult(new ConfigurationProperty(propertyName, this.State.Properties[propertyName]));
        }

        public async Task SetProperty(ConfigurationProperty property)
        {
            this.State.Properties[property.Name] = property.Value;

            await this.WriteStateAsync();
        }

        public async Task RemoveProperty(string propertyName)
        {
            if (this.State.Properties.ContainsKey(propertyName))
            {
                this.State.Properties.Remove(propertyName);

                await this.WriteStateAsync();
            }
        }

        public class DeploymentConfigurationState
        {
            public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        }
    }
}