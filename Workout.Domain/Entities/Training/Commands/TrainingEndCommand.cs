using Workout.Domain.Core.Commands;

namespace Workout.Domain.Entities.Training.Commands
{
    public class TrainingEndCommand : Command
    {
        public int UserId { get; private set; }
        public int Code { get; private set; }
        public char Type { get; private set; }
        protected TrainingEndCommand() { }
        public TrainingEndCommand(int userId, int code, char type)
        {
            UserId = userId;
            Type = type;
            Code = code;
        }
    }
}
