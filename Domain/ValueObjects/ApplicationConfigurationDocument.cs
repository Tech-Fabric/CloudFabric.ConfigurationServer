using CloudFabric.ConfigurationServer.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddEnvironment(EnvironmentConfigurationDocument environment)
        {
            if(Environments.Exists(e => e.Name.Equals(environment.Name)))
            {
                throw new EnvironmentAlreadyExistsException(environment);
            }
            Environments.Add(environment);
        }

        public List<ConfigurationProperty> GetTransformedConfiguration(EnvironmentName environmentName)
        {
            var foundEnvironmentConfiguration = Environments.First(e => e.Name.Equals(environmentName));
            if(foundEnvironmentConfiguration != null)
            {
                /*
                 * if envrionment configuration exists, apply the configuration and return it.
                 */ 
                return GetTransformedConfiguration(foundEnvironmentConfiguration.GetProperties());
            }
            else
            {
                /*
                 * If environment configuration doesn't exist, return a copy of the properties.
                 */ 
                return GetProperties();
            }
        }
    }
}
