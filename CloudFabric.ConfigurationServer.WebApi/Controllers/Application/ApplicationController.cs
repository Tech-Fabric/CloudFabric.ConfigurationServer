using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Application
{
    [ApiController]
    [Route("api/clients/{clientName}/applications")]
    public class ApplicationController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public ApplicationController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public async Task<string[]> GetApplications(string clientName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);

            return await client.GetAllApplicationNames();
        }

        [HttpGet("{name}")]
        public async Task<ApplicationDetails> GetApplication(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(name);

            return new ApplicationDetails
            {
                Environments = await application.GetAllEnvironmentNames(),
                Properties = await application.GetAllProperies()
            };
        }

        [HttpPost("{name}")]
        public async Task AddApplication(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);

            await client.AddApplication(name);
        }

        [HttpDelete("{name}")]
        public async Task RemoveApplication(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);

            await client.RemoveApplication(name);
        }

        [HttpGet]
        [Route("{name}/properties")]
        public async Task<ConfigurationProperty[]> GetConfiguration(string clientName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(name);

            return await application.GetAllProperies();
        }

        [HttpGet]
        [Route("{name}/properties/{propertyName}")]
        public async Task<ConfigurationProperty> GetConfigurationProperty(string clientName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(name);

            return await application.GetProperty(propertyName);
        }

        [HttpPut]
        [Route("{name}/properties/{propertyName}")]
        public async Task SetConfigurationProperty(string clientName, string name, string propertyName, [FromBody]SetConfigurationPropertyRequest request)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(name);

            await application.SetProperty(new ConfigurationProperty(propertyName, request.PropertyValue));
        }

        [HttpDelete]
        [Route("{name}/properties/{propertyName}")]
        public async Task DeleteConfigurationProperty(string clientName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(name);

            await application.RemoveProperty(propertyName);
        }
    }
}
