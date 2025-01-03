using Workout.Domain.Entities.User;
using Workout.Domain.Entities.User.Repositories;
using Workout.Infra.Data.Context;

namespace Workout.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(WorkoutDbContext context) : base(context)
        {
        }

        public User? GetByCredentials(string username, string password) => GetAll().Where(item => item.Username == username && item.Password == password)
                                                                                   .FirstOrDefault();
    }
}
