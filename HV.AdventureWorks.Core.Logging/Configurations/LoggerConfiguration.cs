using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HV.AdventureWorks.Core.Logging.Configurations
{
    public static class LoggerConfiguration
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection serviceCollection, string loggingFilePath)
        {
            return serviceCollection.AddSingleton(x =>
            {
                return new LoggerFactory().AddSerilog(SerilogLoggerFactory.Create(loggingFilePath));
            });
        }
    }
}
