using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class Configuration : Grain<Configuration.ConfigurationState>, IConfiguration
    {
        public async Task<IClientConfiguration> AddClient(string clientName)
        {
            if (this.State.Clients.ContainsKey(clientName))
                throw new Exception($"Client {clientName} already exists");

            var clientId = Guid.NewGuid();
            this.State.Clients[clientName] = clientId;

            await this.WriteStateAsync();

            return this.GrainFactory.GetGrain<IClientConfiguration>(clientId);
        }

        public Task<string[]> GetAllClientNames()
        {
            return Task.FromResult(this.State.Clients.Keys.ToArray());
        }

        public Task<IClientConfiguration> GetClient(string clientName)
        {
            if (!this.State.Clients.ContainsKey(clientName))
                throw new Exception($"Client {clientName} doesn't exists");

            return Task.FromResult(this.GrainFactory.GetGrain<IClientConfiguration>(this.State.Clients[clientName]));
        }

        public async Task<ConfigurationProperty[]> GetEffectiveConfiguration(string clientName, string applicationName, string environmentName, string deploymentName = null)
        {
            if (!this.State.Clients.ContainsKey(clientName))
                throw new Exception($"Client {clientName} doesn't exists");

            var client = this.GrainFactory.GetGrain<IClientConfiguration>(this.State.Clients[clientName]);

            var clientProperties = await client.GetEffectiveConfiguration(applicationName, environmentName, deploymentName);

            return clientProperties;
        }

        public Task RemoveClient(string clientName)
        {
            throw new System.NotImplementedException();
        }

        public class ConfigurationState
        {
            public Dictionary<string, Guid> Clients { get; set; } = new Dictionary<string, Guid>();
        }
    }
}
