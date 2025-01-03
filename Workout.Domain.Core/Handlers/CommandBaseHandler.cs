

using Workout.Domain.Core.Interfaces;

namespace Workout.Domain.Core.Handlers
{
    public abstract class CommandBaseHandler
    {
        private readonly IUnitOfWork _uow;
    }
}
