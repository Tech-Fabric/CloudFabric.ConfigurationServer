using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Client
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public ClientController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet("{name}")]
        public async Task<ClientDetails> GetClient(string name)
        {
            var client = await this.OrleansClient.Value.GetGrain<IConfiguration>(0).GetClient(name);

            var clientDetails = new ClientDetails
            {
                Environments = await client.GetAllEnvironmentNames(),
                Applications = await client.GetAllApplicationNames(),
                Properties = await client.GetAllProperies()
            };

            return clientDetails;
        }

        [HttpGet]
        public Task<string[]> GetClients() => this.OrleansClient.Value.GetGrain<IConfiguration>(0).GetAllClientNames();

        [HttpPost("{name}")]
        public Task AddClient(string name) => this.OrleansClient.Value.GetGrain<IConfiguration>(0).AddClient(name);

        [HttpDelete("{name}")]
        public Task RemoveClient(string name) => this.OrleansClient.Value.GetGrain<IConfiguration>(0).RemoveClient(name);

        [HttpGet]
        [Route("{name}/property")]
        public async Task<ConfigurationProperty[]> GetConfiguration(string name)
        {
            var client = await this.OrleansClient.Value.GetGrain<IConfiguration>(0).GetClient(name);

            return await client.GetAllProperies();
        }

        [HttpGet]
        [Route("{name}/property/{propertyName}")]
        public async Task<string> GetConfigurationProperty(string name, string propertyName)
        {
            var client = await this.OrleansClient.Value.GetGrain<IConfiguration>(0).GetClient(name);
            var property = await client.GetProperty(propertyName);

            return property.Value;
        }

        [HttpPut]
        [Route("{name}/property/{propertyName}")]
        public async Task SetConfigurationProperty(string name, string propertyName, [FromBody]SetConfigurationPropertyRequest request)
        {
            var client = await this.OrleansClient.Value.GetGrain<IConfiguration>(0).GetClient(name);

            await client.SetProperty(new ConfigurationProperty(propertyName, request.PropertyValue));
        }

        [HttpDelete]
        [Route("{name}/property/{propertyName}")]
        public async Task DeleteConfigurationProperty(string name, string propertyName)
        {
            var client = await this.OrleansClient.Value.GetGrain<IConfiguration>(0).GetClient(name);
            await client.RemoveProperty(propertyName);
        }
    }
}