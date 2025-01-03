using Workout.Domain.Core.Interfaces;
using Workout.Infra.Data.Context;
using Workout.Infra.Data.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Workout.Infra.IoC.Extensions
{
    public static class ContextExtentions
    {
        public static void AddDbContexts(this IServiceCollection service, IConfiguration configuration)
        {
            //Infra - Data - DbContext
            service.AddScoped<WorkoutDbContext>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
