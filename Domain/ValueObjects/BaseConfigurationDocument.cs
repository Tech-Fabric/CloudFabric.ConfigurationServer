using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddProperty(ConfigurationProperty property)
        {
            Properties.Add(property);
        }
        
        public ConfigurationProperty? GetProperty(string name)
        {
            return Properties.First(p => p.Name.CompareTo(name) == 0);
        }

        public List<ConfigurationProperty> GetProperties()
        {
            return new List<ConfigurationProperty>(Properties);
        }

        protected List<ConfigurationProperty> GetTransformedConfiguration(List<ConfigurationProperty> properties)
        {
            /*
             * Add all exising prop
             */
            var newProperties = Properties.ToList();

            properties.ForEach(transformationProp =>
            {
                var existingConfig = newProperties.Where(newProp => newProp.Name == transformationProp.Name).FirstOrDefault();

                /*
                 * If property already exists in the document, replace it with the new value
                 */
                if (existingConfig.Value != null)
                {
                    newProperties.Remove(existingConfig);
                }
                newProperties.Add(transformationProp);

            });
            return newProperties;
        }
    }
}
