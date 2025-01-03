using MediatR;
using Microsoft.Extensions.Logging;
using Workout.Domain.Core.Handlers;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Entities.Auth.Commands;
using Workout.Domain.Entities.Session.Repositories;

namespace Workout.Service.Auth.Commands
{
    public class SignOutCommandHandler : CommandHandler, IRequestHandler<SignOutCommand>
    {
        private readonly ISessionRepository _repository;
        private readonly ILogger<SignInCommandHandler> _logger; 
        public SignOutCommandHandler(IUnitOfWork uow,
                                     IMediatorHandler mediator,
                                     INotificationHandler<DomainNotification> notifications,
                                     ISessionRepository repository,
                                     ILogger<SignInCommandHandler> logger) : base(uow, mediator, notifications)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                var session = _repository.GetByKey(request.SessionKey);
                if (session == default)
                    ErrorNotification("Sessão não encontrada.");

                 session.Inactivate();
                 session.DoLogout();

                _repository.Update(session);
                _repository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falid do logout. SessionKey:{0}", request.SessionKey);
                ErrorNotification("Não foi possível fazer o logout.");
            }
        }
    }
}
