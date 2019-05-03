using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public class ApplicationConfigurationDocument : BaseConfigurationDocument
    {
        public ApplicationName Name { get; }
        public List<EnvironmentConfigurationDocument> Environments { get; }
        public ApplicationConfigurationDocument(ApplicationName name, List<EnvironmentConfigurationDocument> environments = null, List<ConfigurationProperty> properties = null) : base(properties)
        {
            Environments = new List<EnvironmentConfigurationDocument>();

            Name = name;
            Environments = environments ?? Environments;
        }
    }
}
