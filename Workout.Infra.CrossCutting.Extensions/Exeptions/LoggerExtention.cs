using Microsoft.Extensions.Hosting;
using Serilog;

namespace Workout.Infra.CrossCutting.Extensions.Exeptions
{
    public static class LoggerExtention
    {
        public static void UseLogger(this IHostBuilder host)
        {
            // host.UseSerilog();
        }
    }
}
