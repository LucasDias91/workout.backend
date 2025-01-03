using Workout.Domain.Core.Events;
using MediatR;

namespace Workout.Domain.Core.Queries
{
    public class Query<TResponse> : Message, IRequest<TResponse>
    {
        public DateTime Timestamp { get; private set; }

        public Query()
        {
            Timestamp = DateTime.Now;
        }
    }
}
