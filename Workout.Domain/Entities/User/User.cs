using Workout.Infra.CrossCutting.Exceptions;

namespace Workout.Domain.Entities.User
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string? Avatar { get; private set; }
        public string Key { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        protected User() { }

        public void EnsureIsActive()
        {
            if(!IsActive)
                throw new ForbiddenException();
        }

        public User(string name, string username, string password)
        {
              Key = Guid.NewGuid().ToString();  
              Name = name;
              Username = username;
              Password = password;
             Activate();
        }
    }
}
