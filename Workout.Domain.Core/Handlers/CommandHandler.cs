using Workout.Domain.Core.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Core.Emums;

namespace Workout.Domain.Core.Handlers
{
    public abstract class CommandHandler
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMediatorHandler _mediator;
        protected readonly DomainNotificationHandler _notifications;

        protected CommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void ErrorNotifications(ValidationResult validationResult)
        {
            _mediator.PublishEvent(new DomainNotification(NotificationType.Error, validationResult.ErrorMessage));
        }

        protected void ErrorNotification(string notification)
        {
            _mediator.PublishEvent(new DomainNotification(NotificationType.Error, notification));
        }

        protected void InformationNotification(string notification)
        {
            _mediator.PublishEvent(new DomainNotification(NotificationType.Information, notification));
        }

        protected bool Commit()
        {
            if (_notifications.HasErrors())
                return false;

            if (_uow.Save())
                return true;

            _mediator.PublishEvent(new DomainNotification(NotificationType.Error, "Não foi possivel salvar os dados!"));
            return false;
        }

        protected void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        protected bool CommitTransaction()
        {
            if (_notifications.HasNotifications())
                return false;

            _uow.CommitTransaction();
            return true;
        }

        protected void RollbackTransaction()
        {
            _uow.RollbackTransaction();
        }

        protected Task Completed() => Task.CompletedTask;

    }
}
