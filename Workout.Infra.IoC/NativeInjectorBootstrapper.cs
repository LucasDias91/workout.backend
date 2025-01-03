using Microsoft.Extensions.Configuration;

namespace Workout.Infra.IoC
{
    using Workout.Infra.IoC.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Workout.Infra.Data;
    using Workout.Service;

    public static class NativeInjectorBootstrapper
    {
        public static void Register(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddDbContexts(configuration);
            builder.Services.AddMediator();
            builder.Services.AddHandlers();
            builder.Services.AddRepositories();
            builder.AddConfigurationOptions();
        }
    }
}