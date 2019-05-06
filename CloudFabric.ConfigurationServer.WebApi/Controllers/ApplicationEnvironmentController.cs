using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers
{
    [ApiController]
    [Route("api/client/{clientName}/application/{applicationName}/environment")]
    public class ApplicationEnvironmentController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public ApplicationEnvironmentController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public Task<object[]> GetApplicationEnvironments(string clientName, string applicationName)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{name}")]
        public Task<object> GetApplicationEnvironment(string clientName, string applicationName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{name}")]
        public Task AddApplicationEnvironment(string clientName, string applicationName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{name}")]
        public Task RemoveApplicationEnvironment(string clientName, string applicationName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property")]
        public Task<object[]> GetConfiguration(string clientName, string applicationName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property/{propertyName}")]
        public Task<object> GetConfigurationProperty(string clientName, string applicationName, string name, string propertyName)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{name}/property/{propertyName}")]
        public Task SetConfigurationProperty(string clientName, string applicationName, string name, string propertyName, [FromBody]string propertyValue)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{name}/property/{propertyName}")]
        public Task DeleteConfigurationProperty(string clientName, string applicationName, string name, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
