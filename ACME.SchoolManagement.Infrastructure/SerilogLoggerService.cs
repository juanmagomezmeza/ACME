using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using Serilog;
using Serilog.Events;

namespace ACME.SchoolManagement.Infrastructure
{
    public class SerilogLoggerService : ILoggerService
    {

        public SerilogLoggerService()
        {

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.File("Logs/webapi-.log", rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3} {Username} {Message:lj}{Exception}{NewLine}")
                        .Enrich.FromLogContext()
                        .CreateLogger();
        }

        public void Error(string message, object value)
        {
            Log.Error(message, value);
        }

        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Warning(string message)
        {
            Log.Warning(message);
        }
    }
}
