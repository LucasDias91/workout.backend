using Workout.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Domain.Core.Notifications
{
    public class ApplicationNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }

        public string Value { get; private set; }
        public Exception Exception { get; private set; }

        public ApplicationNotification(string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Value = value;
        }
        public ApplicationNotification(Exception exception)
        {
            DomainNotificationId = Guid.NewGuid();
            Value = exception.Message;
        }
    }
}
