using Workout.Domain.Core.Interfaces;

namespace Workout.Domain.Entities.Session.Repositories
{
    public interface ISessionRepository : IRepositoryBase<Session>
    {
        public Session? GetByKey(string key);
    }
}
