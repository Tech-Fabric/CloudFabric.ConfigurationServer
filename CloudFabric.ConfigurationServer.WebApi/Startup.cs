using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace CloudFabric.ConfigurationServer.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton(new Lazy<IClusterClient>(() =>
            {
                var client = CreateOrleansClient();
                client.Connect(e => Task.FromResult(true)).Wait();

                return client;
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Configuration Service API", Version = "v1" });
                c.CustomSchemaIds(CustomSchemaIdProvider);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Configuration Service API");
            });

            app.UseMvc();
        }

        private static IClusterClient CreateOrleansClient()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "ConfigurationService";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            return client;
        }

        private static string CustomSchemaIdProvider(Type type)
        {
            const string controllerNamespace = "CloudFabric.ConfigurationServer.WebApi.Controllers.";

            if (type.FullName.StartsWith(controllerNamespace))
                return type.FullName.Substring("CloudFabric.ConfigurationServer.WebApi.Controllers.".Length);
            else
                return type.Name;
        }
    }
}
