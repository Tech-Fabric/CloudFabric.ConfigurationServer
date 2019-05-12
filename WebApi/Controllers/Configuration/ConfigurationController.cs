using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Configuration
{
    [ApiController]
    [Route("api/configuration/clients/{clientName}/applications/{applicationName}/environments/{environmentName}")]
    public class ConfigurationController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public ConfigurationController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public async Task<ConfigurationProperty[]> GetEffectiveConfiguration(string clientName, string applicationName, string environmentName, string deploymentName)
        {
            return await this.OrleansClient.Value.GetConfigurationGrain().GetEffectiveConfiguration(clientName, applicationName, environmentName, deploymentName);
        }
    }
}
