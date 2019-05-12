using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Environment
{
    [ApiController]
    [Route("api/clients/{clientName}/environments")]
    public class EnvironmentController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public EnvironmentController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public async Task<string[]> GetEnvironments(string clientName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);

            return await client.GetAllEnvironmentNames();
        }

        [HttpGet("{name}")]
        public async Task<EnvironmentDetails> GetEnvironment(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var environment = await client.GetEnvironment(name);

            return new EnvironmentDetails
            {
                Properties = await environment.GetAllProperies()
            };
        }

        [HttpPost("{name}")]
        public async Task AddEnvironment(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);

            await client.AddEnvironment(name);
        }

        [HttpDelete("{name}")]
        public async Task RemoveEnvironment(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);

            await client.RemoveEnvironment(name);
        }

        [HttpGet]
        [Route("{name}/properties")]
        public async Task<ConfigurationProperty[]> GetConfiguration(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var environment = await client.GetEnvironment(name);

            return await environment.GetAllProperies();
        }

        [HttpGet]
        [Route("{name}/properties/{propertyName}")]
        public async Task<ConfigurationProperty> GetConfigurationProperty(string clientName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var environment = await client.GetEnvironment(name);

            return await environment.GetProperty(propertyName);
        }

        [HttpPut]
        [Route("{name}/properties/{propertyName}")]
        public async Task SetConfigurationProperty(string clientName, string name, string propertyName, [FromBody]SetConfigurationPropertyRequest request)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var environment = await client.GetEnvironment(name);

            await environment.SetProperty(new ConfigurationProperty(propertyName, request.PropertyValue));
        }

        [HttpDelete]
        [Route("{name}/properties/{propertyName}")]
        public async Task DeleteConfigurationProperty(string clientName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var environment = await client.GetEnvironment(name);

            await environment.RemoveProperty(propertyName);
        }
    }
}
