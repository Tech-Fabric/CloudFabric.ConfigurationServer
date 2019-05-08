using CloudFabric.ConfigurationServer.Grains;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CloudFabric.ConfigurationServer.Silo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "ConfigurationServer";
                })
                .AddCosmosDBGrainStorageAsDefault(options =>
                {
                    options.ConnectionProtocol = Microsoft.Azure.Documents.Client.Protocol.Tcp;
                    options.ConnectionMode = Microsoft.Azure.Documents.Client.ConnectionMode.Direct;
                    options.AccountEndpoint = "https://localhost:8081";
                    options.AccountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                    options.Collection = "ConfigurationServer";
                    options.DB = "CloudFabric";
                    options.CollectionThroughput = 1000;
                    options.CanCreateResources = true;
                    options.AutoUpdateStoredProcedures = true;
                    options.DeleteStateOnClear = true;
                    options.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
                    options.UseFullAssemblyNames = false;
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Configuration).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();

            Console.WriteLine("Press any key to stop the silo");
            Console.ReadKey();

            await host.StopAsync();
        }
    }
}
