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

            var client = this.GrainFactory.GetGrain<IClientConfiguration>(clientId);
            await client.Create(clientName);

            await this.WriteStateAsync();

            return client;
        }

        public Task<string[]> GetAllClientNames() => Task.FromResult(this.State.Clients.Keys.ToArray());

        public Task<IClientConfiguration> GetClient(string clientName)
        {
            if (!this.State.Clients.ContainsKey(clientName))
                throw new Exception($"Client {clientName} doesn't exists");

            return Task.FromResult(this.GrainFactory.GetGrain<IClientConfiguration>(this.State.Clients[clientName]));
        }

        public async Task RemoveClient(string clientName)
        {
            if (!this.State.Clients.ContainsKey(clientName))
                return;

            await this.GrainFactory.GetGrain<IClientConfiguration>(this.State.Clients[clientName]).Delete();

            this.State.Clients.Remove(clientName);

            await this.WriteStateAsync();
        }

        public async Task<ConfigurationProperty[]> GetEffectiveConfiguration(string clientName, string applicationName, string environmentName, string deploymentName = null)
        {
            if (!this.State.Clients.ContainsKey(clientName))
                throw new Exception($"Client {clientName} doesn't exists");

            var client = this.GrainFactory.GetGrain<IClientConfiguration>(this.State.Clients[clientName]);

            var clientProperties = await client.GetEffectiveConfiguration(applicationName, environmentName, deploymentName);

            return clientProperties;
        }

        public class ConfigurationState
        {
            public Dictionary<string, Guid> Clients { get; set; } = new Dictionary<string, Guid>();
        }
    }
}
