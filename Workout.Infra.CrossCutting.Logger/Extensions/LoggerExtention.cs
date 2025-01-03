using Microsoft.Extensions.Hosting;
using Serilog;

namespace Workout.Infra.CrossCutting.Logger.Extensions
{
    public static class LoggerExtention
    {
        public static void UseLogger(this IHostBuilder host)
        {
            host.UseSerilog();
        }
    }
}
