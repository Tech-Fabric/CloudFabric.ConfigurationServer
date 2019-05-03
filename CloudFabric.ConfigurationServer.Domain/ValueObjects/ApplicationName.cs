using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public struct ApplicationName
    {
        public string Value { get; }
        public ApplicationName(string value)
        {
            Value = value;
        }
    }
}
