using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.Exceptions
{
    public class EnvironmentAlreadyExistsException : Exception
    {
        public EnvironmentAlreadyExistsException(EnvironmentConfigurationDocument environment) : base($"An environment configuration already exists with the name: {environment.Name}.") { }
    }
}
