using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public struct ConfigurationMetadata
    {
        public ApplicationName ApplicationName { get; }
        public EnvironmentName EnvironmentName { get; }
    }
}
