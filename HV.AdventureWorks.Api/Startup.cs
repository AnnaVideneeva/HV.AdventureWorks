using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using HV.AdventureWorks.Services.Configurations;
using HV.AdventureWorks.Core.Logging.Configurations;
using HV.AdventureWorks.AzureStorage.Configurations;
using HV.AdventureWorks.AzureStorage;

namespace HV.AdventureWorks.Api
{
    public class Startup
    {
        private const string ConnectionStringKey = "DefaultConnection";
        private const string AzureStorageConnectionStringKey = "AzureStorageConnectionString";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var azureStorageConnectionString = Configuration.GetValue<string>(AzureStorageConnectionStringKey);

            services
                .ConfigureMapper()
                .ConfigureServices(Configuration.GetConnectionString(ConnectionStringKey))
                .ConfigureAzureStorage(azureStorageConnectionString)
                .ConfigureLogger(StorageAccountFactory.Create(azureStorageConnectionString));

            ConfigureSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adventure Works");
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Adventure Works"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
