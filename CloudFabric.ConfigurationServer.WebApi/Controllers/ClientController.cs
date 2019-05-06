using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers
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

        [HttpGet]
        public Task<object[]> GetClients()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{name}")]
        public Task<object> GetClient(string name)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{name}")]
        public Task AddClient(string name)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{name}")]
        public Task RemoveClient(string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property")]
        public Task<object[]> GetConfiguration(string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}/property/{propertyName}")]
        public Task<object> GetConfigurationProperty(string name, string propertyName)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{name}/property/{propertyName}")]
        public Task SetConfigurationProperty(string name, string propertyName, [FromBody]string propertyValue)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{name}/property/{propertyName}")]
        public Task DeleteConfigurationProperty(string name, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
