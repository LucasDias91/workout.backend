using MediatR;

namespace Workout.Domain.Core.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
            where TQuery : Query<TResponse>
    {
    }
}
