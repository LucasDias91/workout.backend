using Workout.Domain.Core.Interfaces;

namespace Workout.Domain.Entities.Training.Repositories
{
    public interface ITrainingRepository : IRepositoryBase<Training>
    {
        IEnumerable<Training> GetByCode(int code);
    }
}
