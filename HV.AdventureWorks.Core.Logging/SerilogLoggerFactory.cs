using Serilog;

namespace HV.AdventureWorks.Core.Logging
{
    public static class SerilogLoggerFactory
    {
        public static ILogger Create(string loggingFilePath)
        {
            var configuration = new LoggerConfiguration()
                .WriteTo.File(loggingFilePath);

            return configuration.CreateLogger();
        }
    }
}
