using Microsoft.Extensions.DependencyInjection;

namespace HV.AdventureWorks.AzureStorage.Configurations
{
    public static class AzureStorageConfiguration
    {
        public static IServiceCollection ConfigureAzureStorage(this IServiceCollection serviceCollection, string azureStorageConnectionString)
        {
            return serviceCollection
                .AddTransient<IBlobService, BlobService>(s => new BlobService(azureStorageConnectionString))
                .AddTransient<IQueueService, QueueService>(s => new QueueService(azureStorageConnectionString));
        }
    }
}
