using Workout.Domain.Entities.Training;
using Workout.Domain.Entities.Training.Repositories;
using Workout.Infra.Data.Context;

namespace Workout.Infra.Data.Repositories
{
    public class TrainingRepository : RepositoryBase<Training>, ITrainingRepository
    {
        public TrainingRepository(WorkoutDbContext context) : base(context)
        {
        }

        public IEnumerable<Training> GetByCode(int code)
            => GetAll().Where(item => item.Code == code);
      
    }
}
