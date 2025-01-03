using Workout.Domain.Core.Emums;
using Workout.Domain.Core.Events;

namespace Workout.Domain.Core.Notifications
{

    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }

        public string Value { get; private set; }

        public NotificationType Type { get; private set; }

        public DomainNotification(NotificationType type, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Type = type;
            Value = value;
        }
    }
}
