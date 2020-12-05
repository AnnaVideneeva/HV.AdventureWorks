using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Serilog;

namespace HV.AdventureWorks.Core.Logging.Configurations
{
    public static class LoggerConfiguration
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection serviceCollection, CloudStorageAccount storage)
        {
            return serviceCollection.AddSingleton(x =>
            {
                return new LoggerFactory().AddSerilog(SerilogLoggerFactory.Create(storage));
            });
        }
    }
}
