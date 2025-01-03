using Workout.Domain.Core.Interfaces;

namespace Workout.Domain.Entities.User.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User? GetByCredentials(string username, string password);
    }
}
