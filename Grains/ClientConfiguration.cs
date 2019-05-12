using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class ClientConfiguration : Grain<ClientConfiguration.ClientConfigurationState>, IClientConfiguration
    {
        public async Task Create(string name)
        {
            this.State.Name = name;

            await this.WriteStateAsync();
        }

        public Task<IEnvironmentConfiguration> GetEnvironment(string environmentName)
        {
            if (!this.State.Environments.ContainsKey(environmentName))
                throw new Exception();

            return Task.FromResult(this.GrainFactory.GetGrain<IEnvironmentConfiguration>(this.State.Environments[environmentName]));
        }

        public Task<string[]> GetAllEnvironmentNames() => Task.FromResult(this.State.Environments.Keys.ToArray());

        public async Task<IEnvironmentConfiguration> AddEnvironment(string environmentName)
        {
            if (this.State.Environments.ContainsKey(environmentName))
                throw new Exception();

            var environmentId = Guid.NewGuid();
            this.State.Environments[environmentName] = environmentId;

            await this.WriteStateAsync();

            return this.GrainFactory.GetGrain<IEnvironmentConfiguration>(environmentId);
        }

        public async Task RemoveEnvironment(string environmentName)
        {
            if (!this.State.Environments.ContainsKey(environmentName))
                return;

            await this.GrainFactory.GetGrain<IEnvironmentConfiguration>(this.State.Environments[environmentName]).Delete();

            this.State.Environments.Remove(environmentName);

            await this.WriteStateAsync();
        }

        public Task<string[]> GetAllApplicationNames() => Task.FromResult(this.State.Applications.Keys.ToArray());

        public Task<IApplicationConfiguration> GetApplication(string applicationName)
        {
            if (!this.State.Applications.ContainsKey(applicationName))
                throw new Exception();

            return Task.FromResult(this.GrainFactory.GetGrain<IApplicationConfiguration>(this.State.Applications[applicationName]));
        }

        public async Task<IApplicationConfiguration> AddApplication(string applicationName)
        {
            if (this.State.Applications.ContainsKey(applicationName))
                throw new Exception();

            var applicationId = Guid.NewGuid();
            this.State.Environments[applicationName] = applicationId;

            await this.WriteStateAsync();

            return this.GrainFactory.GetGrain<IApplicationConfiguration>(applicationId);
        }

        public async Task RemoveApplication(string applicationName)
        {
            if (!this.State.Environments.ContainsKey(applicationName))
                return;

            await this.GrainFactory.GetGrain<IEnvironmentConfiguration>(this.State.Environments[applicationName]).Delete();

            this.State.Environments.Remove(applicationName);

            await this.WriteStateAsync();
        }

        public async Task<ConfigurationProperty[]> GetEffectiveConfiguration(string applicationName, string environmentName, string deploymentName = null)
        {
            var application = await this.GetApplication(applicationName);
            var environment = await this.GetEnvironment(environmentName);

            var clientConfiguration = await this.GetAllProperies();
            var applicationConfiguration = await application.GetEffectiveConfiguration(environmentName, deploymentName);
            var environmentConfiguration = await environment.GetAllProperies();

            return environmentConfiguration.OverrideWith(clientConfiguration).OverrideWith(applicationConfiguration).ToArray();
        }

        public async Task Delete()
        {
            var deleteApplications = this.State.Applications.Values.Select(x => this.GrainFactory.GetGrain<IApplicationConfiguration>(x).Delete());
            var deleteEnvironments = this.State.Environments.Values.Select(x => this.GrainFactory.GetGrain<IEnvironmentConfiguration>(x).Delete());

            await Task.WhenAll(deleteApplications);
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

        public class ClientConfigurationState
        {
            public string Name { get; set; }
            public Dictionary<string, Guid> Applications { get; set; } = new Dictionary<string, Guid>();
            public Dictionary<string, Guid> Environments { get; set; } = new Dictionary<string, Guid>();
            public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        }
    }
}
