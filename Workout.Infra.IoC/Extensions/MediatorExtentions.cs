using Workout.Domain.Core.Handlers;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Core.Pipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Workout.Infra.IoC.Extensions
{
    public static class MediatorExtentions
    {
        public static void AddMediator(this IServiceCollection service)
        {
            // Domain Bus (Mediator)
            service.AddScoped<IMediatorHandler, MediatorHandler>();
            service.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorPipeline<,>));

            // Domain - Eventos
            service.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            service.AddScoped<INotificationHandler<ApplicationNotification>, ApplicationNotificationHandler>();
        }
    }
}
