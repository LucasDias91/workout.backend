using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Workout.Infra.CrossCutting.Extensions.Config
{
    public static class ProgramExtention
    {

        public static void AddApiConfiguration(this IServiceCollection service)
        {
            //host.ConfigureAppConfiguration((ctx, config) =>
            //{
            //    config.AddJsonFile("appsettings.json",
            //                       optional: true,
            //                       reloadOnChange: true)
            //          .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true);
            //});


           // service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}
