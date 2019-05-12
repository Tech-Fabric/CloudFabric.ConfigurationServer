using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.TestKit;

namespace CloudFabric.ConfigurationServer.Domain.Tests
{
    public class SiloContext
    {
        public readonly TestKitSilo Silo = new TestKitSilo();

        private Grain grain;

        public async Task<TGrain> GetGrain<TGrain>(long id)
            where TGrain : Grain, IGrainWithIntegerKey
        {
            if (grain == null)
                grain = await this.Silo.CreateGrainAsync<TGrain>(id);

            return (TGrain)grain;
        }

        public async Task<TGrain> GetGrain<TGrain>(Guid id)
            where TGrain : Grain, IGrainWithGuidKey
        {
            if (grain == null)
                grain = await this.Silo.CreateGrainAsync<TGrain>(id);

            return (TGrain)grain;
        }

        public async Task<TGrain> GetGrain<TGrain>(string id)
            where TGrain : Grain, IGrainWithStringKey
        {
            if (grain == null)
                grain = await this.Silo.CreateGrainAsync<TGrain>(id);

            return (TGrain)grain;
        }

        public async Task<TGrain> GetGrain<TGrain>(Guid id, string keyExtension)
            where TGrain : Grain, IGrainWithGuidCompoundKey
        {
            if (grain == null)
                grain = await this.Silo.CreateGrainAsync<TGrain>(id, keyExtension);

            return (TGrain)grain;
        }

        public async Task<TGrain> GetGrain<TGrain>(long id, string keyExtension)
            where TGrain : Grain, IGrainWithIntegerCompoundKey
        {
            if (grain == null)
                grain = await this.Silo.CreateGrainAsync<TGrain>(id, keyExtension);

            return (TGrain)grain;
        }
    }
}
