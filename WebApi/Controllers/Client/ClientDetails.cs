using CloudFabric.ConfigurationServer.Domain.ValueObjects;

namespace CloudFabric.ConfigurationServer.WebApi.Controllers.Client
{
    public class ClientDetails
    {
        public string[] Environments { get; set; }
        public string[] Applications { get; set; }
        public ConfigurationProperty[] Properties { get; set; }
    }
}
