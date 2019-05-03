using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public abstract class BaseConfigurationDocument
    {
        protected List<ConfigurationProperty> Properties { get; }

        protected BaseConfigurationDocument(List<ConfigurationProperty> properties = null)
        {
            Properties = new List<ConfigurationProperty>();

            Properties = properties ?? Properties;
        }

        public int GetNumberOfProperties() => Properties.Count;

        public List<ConfigurationProperty> MergeConfigurationProperties(List<ConfigurationProperty> properties)
        {
            /*
             * Add all exising prop
             */
            return null;
        }
    }
}
