using Orleans;

namespace CloudFabric.ConfigurationServer.GrainInterfaces
{
    public static class IGrainFactoryExtensions
    {
        public static IConfiguration GetConfigurationGrain(this IGrainFactory instance) => instance.GetGrain<IConfiguration>(0);
    }
}
