using Workout.Domain.Core.Events;
using MediatR;

namespace Workout.Domain.Core.Commands
{
    public class Command : Message, IRequest
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }

    public class Command<TResponse> : Message, IRequest<TResponse>
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
