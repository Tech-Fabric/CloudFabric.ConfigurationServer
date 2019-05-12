using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Deployment
{
    [ApiController]
    [Route("api/clients/{clientName}/applications/{applicationName}/environments/{environmentName}/deployments")]
    public class DeploymentController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public DeploymentController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public async Task<string[]> GetDeployments(string clientName, string applicationName, string environmentName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);

            return await environment.GetAllDeploymentNames();
        }

        [HttpGet("{name}")]
        public async Task<DeploymentDetails> GetDeployment(string clientName, string applicationName, string environmentName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);
            var deployment = await environment.GetDeployment(name);

            return new DeploymentDetails
            {
                Properties = await deployment.GetAllProperies()
            };
        }

        [HttpPost("{name}")]
        public async Task AddDeployment(string clientName, string applicationName, string environmentName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);

            await environment.AddDeployment(name);
        }

        [HttpDelete("{name}")]
        public async Task RemoveDeployment(string clientName, string applicationName, string environmentName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);

            await environment.RemoveDeployment(name);
        }

        [HttpGet]
        [Route("{name}/properties")]
        public async Task<ConfigurationProperty[]> GetConfiguration(string clientName, string applicationName, string environmentName, string name)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);
            var deploment = await environment.GetDeployment(name);

            return await deploment.GetAllProperies();
        }

        [HttpGet]
        [Route("{name}/properties/{propertyName}")]
        public async Task<ConfigurationProperty> GetConfigurationProperty(string clientName, string applicationName, string environmentName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);
            var deploment = await environment.GetDeployment(name);

            return await deploment.GetProperty(propertyName);
        }

        [HttpPut]
        [Route("{name}/properties/{propertyName}")]
        public async Task SetConfigurationProperty(string clientName, string applicationName, string environmentName, string name, string propertyName, [FromBody]SetConfigurationPropertyRequest request)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);
            var deploment = await environment.GetDeployment(name);

            await deploment.SetProperty(new ConfigurationProperty(propertyName, request.PropertyValue));
        }

        [HttpDelete]
        [Route("{name}/properties/{propertyName}")]
        public async Task DeleteConfigurationProperty(string clientName, string applicationName, string environmentName, string name, string propertyName)
        {
            var configuration = this.OrleansClient.Value.GetConfigurationGrain();

            var client = await configuration.GetClient(clientName);
            var application = await client.GetApplication(applicationName);
            var environment = await application.GetEnvironment(environmentName);
            var deploment = await environment.GetDeployment(name);

            await deploment.RemoveProperty(propertyName);
        }
    }
}
