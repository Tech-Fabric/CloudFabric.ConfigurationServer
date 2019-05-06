using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers
{
    [ApiController]
    [Route("api/client/{clientName}/application/{applicationName}/environment/{environmentName}/deployment")]
    public class DeploymentController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public DeploymentController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public Task<object[]> GetDeployments(string clientName, string applicationName, string environmentName)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{name}")]
        public Task<object> GetDeployment(string clientName, string applicationName, string environmentName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{name}")]
        public Task AddDeployment(string clientName, string applicationName, string environmentName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{name}")]
        public Task RemoveDeployment(string clientName, string applicationName, string environmentName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property")]
        public Task<object[]> GetConfiguration(string clientName, string applicationName, string environmentName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property/{propertyName}")]
        public Task<object> GetConfigurationProperty(string clientName, string applicationName, string environmentName, string name, string propertyName)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{name}/property/{propertyName}")]
        public Task SetConfigurationProperty(string clientName, string applicationName, string environmentName, string name, string propertyName, [FromBody]string propertyValue)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{name}/property/{propertyName}")]
        public Task DeleteConfigurationProperty(string clientName, string applicationName, string environmentName, string name, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
