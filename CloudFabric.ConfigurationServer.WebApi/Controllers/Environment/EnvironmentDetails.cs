using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Environment
{
    public class EnvironmentDetails
    {
        public ConfigurationProperty[] Properties { get; set; }
    }
}
