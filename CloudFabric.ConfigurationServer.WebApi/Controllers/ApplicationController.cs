using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers
{
    [ApiController]
    [Route("api/client/{clientName}/application")]
    public class ApplicationController : ControllerBase
    {
        private readonly Lazy<IClusterClient> OrleansClient;

        public ApplicationController(Lazy<IClusterClient> orleansClient)
        {
            this.OrleansClient = orleansClient;
        }

        [HttpGet]
        public Task<object[]> GetApplications(string clientName)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{name}")]
        public Task<object> GetApplication(string clientName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{name}")]
        public Task AddApplication(string clientName, string name)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{name}")]
        public Task RemoveApplication(string clientName, string name)
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
