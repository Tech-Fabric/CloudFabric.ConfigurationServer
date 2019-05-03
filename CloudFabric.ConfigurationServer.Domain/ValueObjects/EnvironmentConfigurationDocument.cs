using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public class EnvironmentConfigurationDocument : BaseConfigurationDocument
    {
        public EnvironmentName Name { get; }

        public EnvironmentConfigurationDocument(EnvironmentName name, List<ConfigurationProperty> properties = null) : base(properties)
        {
            Name = name;
        }

    }
}
