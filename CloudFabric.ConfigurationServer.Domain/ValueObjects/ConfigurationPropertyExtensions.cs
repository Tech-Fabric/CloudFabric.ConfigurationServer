using System.Collections.Generic;
using System.Linq;

namespace CloudFabric.ConfigurationServer.Domain.ValueObjects
{
    public static class ConfigurationPropertyExtensions
    {
        public static IEnumerable<ConfigurationProperty> OverrideWith(this IEnumerable<ConfigurationProperty> instance, IEnumerable<ConfigurationProperty> overrideWith)
        {
            var result = instance.ToDictionary(x => x.Name, x => x.Value);

            foreach (var item in overrideWith)
                result[item.Name] = item.Value;

            return result.Select(x => new ConfigurationProperty(x.Key, x.Value));
        }
    }
}
