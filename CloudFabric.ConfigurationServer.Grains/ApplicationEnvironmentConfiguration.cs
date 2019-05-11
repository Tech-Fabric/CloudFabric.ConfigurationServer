using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class ApplicationEnvironmentConfiguration : Grain<ApplicationEnvironmentConfiguration.ApplicationEnvironmentConfigurationState>, IApplicationEnvironmentConfiguration
    {
        public Task<IDeploymentConfiguration> GetDeployment(string DeploymentName)
        {
            if (!this.State.Deployments.ContainsKey(DeploymentName))
                throw new Exception();

            return Task.FromResult(this.GrainFactory.GetGrain<IDeploymentConfiguration>(this.State.Deployments[DeploymentName]));
        }

        public Task<string[]> GetAllDeploymentNames() => Task.FromResult(this.State.Deployments.Keys.ToArray());

        public async Task<IDeploymentConfiguration> AddDeployment(string DeploymentName)
        {
            if (this.State.Deployments.ContainsKey(DeploymentName))
                throw new Exception();

            var DeploymentId = Guid.NewGuid();
            this.State.Deployments[DeploymentName] = DeploymentId;

            await this.WriteStateAsync();

            return this.GrainFactory.GetGrain<IDeploymentConfiguration>(DeploymentId);
        }

        public async Task RemoveDeployment(string DeploymentName)
        {
            if (!this.State.Deployments.ContainsKey(DeploymentName))
                return;

            await this.GrainFactory.GetGrain<IDeploymentConfiguration>(this.State.Deployments[DeploymentName]).Delete();

            this.State.Deployments.Remove(DeploymentName);

            await this.WriteStateAsync();
        }

        public async Task<ConfigurationProperty[]> GetEffectiveConfiguration(string deploymentName = null)
        {
            var applicationEnvironmentConfiguration = await this.GetAllProperies();

            if (deploymentName != null)
            {
                var deployment = await this.GetDeployment(deploymentName);

                var deploymentConfiguration = await deployment.GetAllProperies();

                return applicationEnvironmentConfiguration.OverrideWith(deploymentConfiguration).ToArray();
            }
            else
                return applicationEnvironmentConfiguration;
        }

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

        public class ApplicationEnvironmentConfigurationState
        {
            public Dictionary<string, Guid> Deployments { get; set; } = new Dictionary<string, Guid>();
            public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        }
    }
}
