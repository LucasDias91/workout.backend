using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Workout.Domain.Entities.Auth.Commands;
using Workout.Service.Auth.Commands;

namespace Workout.Service
{
    using System.Collections.Generic;
    using Workout.Domain.Entities.Configuration.Commands;
    using Workout.Service.Configuration.Commands;
    using Workout.Service.Training.Commands;
    using Workout.Domain.Entities.Training.Commands;
    using Workout.Domain.Entities.Configuration.Services;
    using Workout.Service.Configuration.Services;

    public static class ServiceModule
    {
        public static void AddHandlers(this IServiceCollection service)
        {
            // Domain - Entities - Handlers
            service.AddScoped<IRequestHandler<SignInCommand, Domain.Entities.Auth.Auth>, SignInCommandHandler>();
            service.AddScoped<IRequestHandler<ConfigurationCommand>, ConfigurationCommandHandler>();
            service.AddScoped<IRequestHandler<TrainingCommand,IEnumerable<Domain.Entities.Training.Training>>,TrainingCommandHandler >();
            service.AddScoped<IRequestHandler<TrainingTimelineCommand, List<Domain.Entities.Training.Timeline?>>, TrainingTimelineCommandHandler>();
            service.AddScoped<IConfigurationService, ConfigurationService>();
            service.AddScoped<IRequestHandler<SignOutCommand>, SignOutCommandHandler>();
            service.AddScoped<IRequestHandler<TrainingStartCommand>, TrainingStartCommandHandler>();
            service.AddScoped<IRequestHandler<TrainingEndCommand>, TrainingEndCommandHandler>();
        }
    }
}
