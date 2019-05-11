using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.ApplicationEnvironment
{
    public class ApplicationEnvironmentDetails
    {
        public string[] Deployments { get; set; }
        public ConfigurationProperty[] Properties { get; set; }
    }
}
