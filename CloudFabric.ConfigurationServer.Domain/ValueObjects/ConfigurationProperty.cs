using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public struct ConfigurationProperty
    {
        public string Name { get; }
        public string Value { get; }
        public ConfigurationProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
