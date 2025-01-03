using Workout.Domain.Core.Commands;
using Workout.Domain.Core.Events;
using Workout.Domain.Core.Queries;

namespace Workout.Domain.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task<TResponse> SendCommand<TRequest, TResponse>(TRequest command) where TRequest : Command<TResponse>;
        Task<TResponse> SendQuery<TRequest, TResponse>(TRequest query) where TRequest : Query<TResponse>;
        Task PublishEvent<T>(T notification) where T : Event;
    }
}
