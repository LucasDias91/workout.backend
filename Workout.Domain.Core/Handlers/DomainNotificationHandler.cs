using Workout.Domain.Core.Emums;
using Workout.Domain.Core.Notifications;
using MediatR;

namespace Workout.Domain.Core.Handlers
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro: {message.Type} - {message.Value}");

            return Task.CompletedTask;
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public virtual bool HasErrors()
        {
            return _notifications.Any(x => x.Type == NotificationType.Error);
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }

    public class ApplicationNotificationHandler : INotificationHandler<ApplicationNotification>
    {
        private List<ApplicationNotification> _notifications;

        public ApplicationNotificationHandler()
        {
            _notifications = new List<ApplicationNotification>();
        }

        public virtual List<ApplicationNotification> GetNotifications()
        {
            return _notifications;
        }

        public Task Handle(ApplicationNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro: - {message.Value}");

            return Task.CompletedTask;
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public virtual bool HasErrors()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = new List<ApplicationNotification>();
        }
    }
}
