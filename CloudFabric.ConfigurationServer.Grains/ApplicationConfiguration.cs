using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class ApplicationConfiguration : Grain<ApplicationConfiguration.ApplicationConfigurationState>, IApplicationConfiguration
    {
        public Task<IApplicationEnvironmentConfiguration> GetEnvironment(string environmentName)
        {
            if (!this.State.Environments.ContainsKey(environmentName))
                throw new Exception();

            return Task.FromResult(this.GrainFactory.GetGrain<IApplicationEnvironmentConfiguration>(this.State.Environments[environmentName]));
        }

        public Task<string[]> GetAllEnvironmentNames() => Task.FromResult(this.State.Environments.Keys.ToArray());

        public async Task<IApplicationEnvironmentConfiguration> AddEnvironment(string environmentName)
        {
            if (this.State.Environments.ContainsKey(environmentName))
                throw new Exception();

            var environmentId = Guid.NewGuid();
            this.State.Environments[environmentName] = environmentId;

            await this.WriteStateAsync();

            return this.GrainFactory.GetGrain<IApplicationEnvironmentConfiguration>(environmentId);
        }

        public async Task RemoveEnvironment(string environmentName)
        {
            if (!this.State.Environments.ContainsKey(environmentName))
                return;

            await this.GrainFactory.GetGrain<IEnvironmentConfiguration>(this.State.Environments[environmentName]).Delete();

            this.State.Environments.Remove(environmentName);

            await this.WriteStateAsync();
        }

        public async Task<ConfigurationProperty[]> GetEffectiveConfiguration(string environmentName, string deploymentName = null)
        {
            var environment = await this.GetEnvironment(environmentName);

            var applicationConfiguration = await this.GetAllProperies();
            var environmentConfiguration = await environment.GetAllProperies();

            return applicationConfiguration.OverrideWith(environmentConfiguration).ToArray();
        }

        public async Task Delete()
        {
            var deleteEnvironments = this.State.Environments.Values.Select(x => this.GrainFactory.GetGrain<IApplicationEnvironmentConfiguration>(x).Delete());

            await Task.WhenAll(deleteEnvironments);

            await this.ClearStateAsync();
        }

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

        public class ApplicationConfigurationState
        {
            public Dictionary<string, Guid> Environments { get; set; } = new Dictionary<string, Guid>();
            public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        }
    }
}
