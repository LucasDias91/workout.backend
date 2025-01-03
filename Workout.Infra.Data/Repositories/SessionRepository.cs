using Workout.Domain.Entities.Session;
using Workout.Domain.Entities.Session.Repositories;
using Workout.Infra.Data.Context;

namespace Workout.Infra.Data.Repositories
{
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        public SessionRepository(WorkoutDbContext context) : base(context)
        {
        }

        public Session? GetByKey(string key) => GetAll().Where(item=> item.Key == key).FirstOrDefault();
    }
}
