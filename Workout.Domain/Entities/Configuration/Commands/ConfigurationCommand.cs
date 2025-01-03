using Workout.Domain.Core.Commands;

namespace Workout.Domain.Entities.Configuration.Commands
{
    public class ConfigurationCommand : Command
    {
        public string Location { get; private set; }

        public ConfigurationCommand(string location)
        {
            Location = location;
        }
    }
}
