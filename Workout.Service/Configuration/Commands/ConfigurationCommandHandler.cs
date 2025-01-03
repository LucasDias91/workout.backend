using MediatR;
using Workout.Domain.Core.Handlers;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Entities.Configuration.Commands;

namespace Workout.Service.Configuration.Commands
{
    using Workout.Domain.Entities.Configuration.Services;
    public class ConfigurationCommandHandler : CommandHandler, IRequestHandler<ConfigurationCommand>
    {
        private readonly IConfigurationService _configurationService;
        public ConfigurationCommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, IConfigurationService configurationService) : base(uow, mediator, notifications)
        {
            _configurationService = configurationService;
        }

        public Task Handle(ConfigurationCommand request, CancellationToken cancellationToken)
        {
                _uow.BeginTransaction();

                _configurationService.ImportUsers(request.Location);

                _configurationService.ImportTrainings(request.Location);

                _uow.CommitTransaction();

                return Task.CompletedTask;
        }
    }
}
