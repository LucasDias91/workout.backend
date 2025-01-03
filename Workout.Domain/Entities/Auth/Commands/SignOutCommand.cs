
using Workout.Domain.Core.Commands;

namespace Workout.Domain.Entities.Auth.Commands
{
    public class SignOutCommand : Command
    {
        public string? SessionKey { get; private set; }
        protected SignOutCommand() { }

        public SignOutCommand(string? sessionKey)
        {
            SessionKey = sessionKey;
        }
    }
}
