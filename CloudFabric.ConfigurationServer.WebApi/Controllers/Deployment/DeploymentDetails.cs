using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Deployment
{
    public class DeploymentDetails
    {
        public ConfigurationProperty[] Properties { get; set; }
    }
}
