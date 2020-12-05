using Microsoft.WindowsAzure.Storage;
using Serilog;

namespace HV.AdventureWorks.Core.Logging
{
    public static class SerilogLoggerFactory
    {
        public static ILogger Create(CloudStorageAccount storage)
        {
            var configuration = new LoggerConfiguration()
                .WriteTo.AzureTableStorage(storage);

            return configuration.CreateLogger();
        }
    }
}
