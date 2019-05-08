using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Orleans;

namespace CloudFabric.ConfigurationServer.Grains
{
    public class Client : Grain<Client.ClientState>, IClientConfiguration
    {
        public Task<IApplicationConfiguration> AddApplication(string applicationName)
        {
            throw new NotImplementedException();
        }

        public Task<IClientConfiguration> AddClient(string clientName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnvironmentConfiguration> AddEnvironment(string environmentName)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> GetAllApplicationNames()
        {
            throw new NotImplementedException();
        }

        public Task<string[]> GetAllClientNames()
        {
            throw new NotImplementedException();
        }

        public Task<string[]> GetAllEnvironmentNames()
        {
            throw new NotImplementedException();
        }

        public Task<ConfigurationProperty> GetAllProperies()
        {
            throw new NotImplementedException();
        }

        public Task<IApplicationConfiguration> GetApplication(string applicationName)
        {
            throw new NotImplementedException();
        }

        public Task<IClientConfiguration> GetClient(string clientName)
        {
            throw new NotImplementedException();
        }

        public Task<ConfigurationProperty[]> GetEffectiveConfiguration(string clientName, string applicationName, string environmentName, string deploymentName = null)
        {
            throw new NotImplementedException();
        }

        public Task<ConfigurationProperty[]> GetEffectiveConfiguration(string applicationName, string environmentName, string deploymentName = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnvironmentConfiguration> GetEnvironment(string environmentName)
        {
            throw new NotImplementedException();
        }

        public Task<ConfigurationProperty> GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveApplication(string applicationName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClient(string clientName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveEnvironment(string environmentName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public Task SetProperty(string propertyName, string value)
        {
            throw new NotImplementedException();
        }

        public class ClientState
        {
            public Dictionary<string, Guid> Applications { get; set; } = new Dictionary<string, Guid>();
            public Dictionary<string, Guid> Environments { get; set; } = new Dictionary<string, Guid>();
        }
    }
}
