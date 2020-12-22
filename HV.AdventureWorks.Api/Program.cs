using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace HV.AdventureWorks.Api
{
    public class Program
    {
        private const string KeyVaultUriKey = "KeyVaultUri";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();
                    var keyVaultUri = builtConfig[KeyVaultUriKey];

                    var keyVaultClient = new KeyVaultClient(
                        new KeyVaultClient.AuthenticationCallback(
                            new AzureServiceTokenProvider().KeyVaultTokenCallback));
  
                    config.AddAzureKeyVault(keyVaultUri, keyVaultClient, new DefaultKeyVaultSecretManager());
                });
    }
}
