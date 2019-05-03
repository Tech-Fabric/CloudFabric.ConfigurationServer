using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public class ConfigurationDocument : BaseConfigurationDocument
    {
        private List<ApplicationConfigurationDocument> Applications { get; }
        private List<EnvironmentConfigurationDocument> Environments { get; }

        public ConfigurationDocument(List<ApplicationConfigurationDocument> applications = null, List<EnvironmentConfigurationDocument> environments = null, List<ConfigurationProperty> properties = null) : base(properties)
        {
            Applications = new List<ApplicationConfigurationDocument>();
            Environments = new List<EnvironmentConfigurationDocument>();

            Applications = applications ?? Applications;
            Environments = environments ?? Environments;
        }
    }
}
