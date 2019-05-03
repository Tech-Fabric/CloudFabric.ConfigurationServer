using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public struct EnvironmentName
    {
        public string Value { get; }
        public EnvironmentName(string value)
        {
            Value = value;
        }
    }
}
