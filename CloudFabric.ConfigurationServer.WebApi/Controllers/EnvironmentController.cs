using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers
{
    [ApiController]
    [Route("api/client/{clientName}/environment")]
    public class EnvironmentController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public EnvironmentController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public Task<object[]> GetEnvironments(string clientName)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{name}")]
        public Task<object> GetEnvironment(string clientName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{name}")]
        public Task AddEnvironment(string clientName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{name}")]
        public Task RemoveEnvironment(string clientName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property")]
        public Task<object[]> GetConfiguration(string clientName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property/{propertyName}")]
        public Task<object> GetConfigurationProperty(string clientName, string name, string propertyName)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{name}/property/{propertyName}")]
        public Task SetConfigurationProperty(string clientName, string name, string propertyName, [FromBody]string propertyValue)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{name}/property/{propertyName}")]
        public Task DeleteConfigurationProperty(string clientName, string name, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
