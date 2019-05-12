using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Application
{
    public class ApplicationDetails
    {
        public string[] Environments { get; set; }
        public ConfigurationProperty[] Properties { get; set; }
    }
}
