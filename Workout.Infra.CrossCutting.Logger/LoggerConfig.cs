using Microsoft.Extensions.Configuration;
using Serilog;
namespace Workout.Infra.CrossCutting.Logger
{
    using Microsoft.Extensions.Logging;
    public static class LoggerConfig
    {
        public static ILoggerFactory ConfigSerilog(this IConfiguration configuration)
        {
            try
            {
                var serilogLogger = new LoggerConfiguration()
                                            .ReadFrom.Configuration(configuration)
                                            .CreateLogger();

                return new LoggerFactory().AddSerilog(serilogLogger);

            }
            catch (Exception ex)
            {
                Log.Error($"Description: 'Não foi possivel iniciar o seriLog';Message:{ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}