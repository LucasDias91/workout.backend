using Microsoft.Extensions.DependencyInjection;
using Workout.Domain.Entities.Configuration.Repositories;
using Workout.Domain.Entities.Session.Repositories;
using Workout.Domain.Entities.Training.Repositories;
using Workout.Domain.Entities.User.Repositories;
using Workout.Infra.Data.Repositories;
namespace Workout.Infra.Data
{
    public static class RepositoryModule
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ISessionRepository, SessionRepository>();
            service.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            service.AddScoped<ITrainingRepository, TrainingRepository>();
            service.AddScoped<ITimelineRepository, TimelineRepository>();
        }
    }
}
