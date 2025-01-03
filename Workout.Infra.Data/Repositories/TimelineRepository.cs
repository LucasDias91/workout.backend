using Workout.Domain.Entities.Training;
using Workout.Domain.Entities.Training.Repositories;
using Workout.Infra.Data.Context;

namespace Workout.Infra.Data.Repositories
{
    public class TimelineRepository : RepositoryBase<Timeline>, ITimelineRepository
    {
        public TimelineRepository(WorkoutDbContext context) : base(context)
        {
        }
    }
}
