using HV.AdventureWorks.AppFunctions;
using HV.AdventureWorks.AzureStorage.Configurations;
using HV.AdventureWorks.Services.Configurations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace HV.AdventureWorks.AppFunctions
{
    public class Startup : FunctionsStartup
    {
        private const string AzureStorageConnectionStringKey = "AzureStorageConnectionString";
        private const string DatabaseConnectionStringKey = "DatabaseConnectionString";

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var azureStorageConnectionString = System.Environment.GetEnvironmentVariable(AzureStorageConnectionStringKey);
            var databaseConnectionString = System.Environment.GetEnvironmentVariable(DatabaseConnectionStringKey);

            builder.Services
                .ConfigureMapper()
                .ConfigureServices(databaseConnectionString)
                .ConfigureAzureStorage(azureStorageConnectionString);
        }
    }
}
