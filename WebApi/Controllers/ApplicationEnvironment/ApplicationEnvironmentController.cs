using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.ApplicationEnvironment
{
    [ApiController]
    [Route("api/clients/{clientName}/applications/{applicationName}/environments")]
    public class ApplicationEnvironmentController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public ApplicationEnvironmentController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public async Task<string[]> GetApplicationEnvironments(string clientName, string applicationName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);

            return await application.GetAllEnvironmentNames();
        }

        [HttpGet("{name}")]
        public async Task<ApplicationEnvironmentDetails> GetApplicationEnvironment(string clientName, string applicationName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(name);

            return new ApplicationEnvironmentDetails
            {
                Deployments = await environment.GetAllDeploymentNames(),
                Properties = await environment.GetAllProperies()
            };
        }

        [HttpPost("{name}")]
        public async Task AddApplicationEnvironment(string clientName, string applicationName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);

            await application.AddEnvironment(name);
        }

        [HttpDelete("{name}")]
        public async Task RemoveApplicationEnvironment(string clientName, string applicationName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);

            await application.RemoveEnvironment(name);
        }

        [HttpGet]
        [Route("{name}/properties")]
        public async Task<ConfigurationProperty[]> GetConfiguration(string clientName, string applicationName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(name);

            return await environment.GetAllProperies();
        }

        [HttpGet]
        [Route("{name}/properties/{propertyName}")]
        public async Task<ConfigurationProperty> GetConfigurationProperty(string clientName, string applicationName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(name);

            return await environment.GetProperty(propertyName);
        }

        [HttpPut]
        [Route("{name}/properties/{propertyName}")]
        public async Task SetConfigurationProperty(string clientName, string applicationName, string name, string propertyName, [FromBody]SetConfigurationPropertyRequest request)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(name);

            await environment.SetProperty(new ConfigurationProperty(propertyName, request.PropertyValue));
        }

        [HttpDelete]
        [Route("{name}/properties/{propertyName}")]
        public async Task DeleteConfigurationProperty(string clientName, string applicationName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(name);

            await environment.RemoveProperty(propertyName);
        }
    }
}
