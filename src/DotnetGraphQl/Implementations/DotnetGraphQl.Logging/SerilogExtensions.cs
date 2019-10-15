using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace DotnetGraphQl.Logging
{
    public static class SerilogExtensions
    {
        public static ILogger CreateSerilogger(IConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration();
            ConfigureSerilogger(configuration, loggerConfiguration);
            return loggerConfiguration.CreateLogger();
        }
        
        private static void ConfigureSerilogger(IConfiguration configuration, LoggerConfiguration loggerConfiguration)
        {
            const string outputTemplate =
                "{Timestamp:yyyyMMdd HH:mm:ss,fff};{CallId};{ThreadId};{SourceContext:l};{Message:lj}{NewLine}{Exception}";

            loggerConfiguration
                .MinimumLevel.Information()
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .ReadFrom.Configuration(configuration)
                .WriteTo.Logger(config => config
                    .WriteTo.Console(outputTemplate: outputTemplate)
                    .Enrich.FromLogContext()
                    .Enrich.WithThreadId()
                );
        }
    }
}