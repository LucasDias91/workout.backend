using Workout.Domain.Core.Commands;
using Workout.Domain.Core.Events;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Queries;
using MediatR;

namespace Workout.Domain.Core.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendCommand<T>(T comando) where T : Command
        {
            await _mediator.Send(comando);
        }

        public async Task<TResponse> SendCommand<TRequest, TResponse>(TRequest comando) where TRequest : Command<TResponse>
        {
            return await _mediator.Send(comando);
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task<TResponse> SendQuery<TRequest, TResponse>(TRequest query) where TRequest : Query<TResponse>
        {
            return await _mediator.Send(query);
        }
    }
}
