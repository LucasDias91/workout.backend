using Workout.Domain.Core.Commands;

namespace Workout.Domain.Entities.Training.Commands
{
    public class TrainingCommand : Command<IEnumerable<Training>>
    {
        public int Code { get; private set; }
        public char? Type { get; private set; }
        public TrainingCommand() { }

        public TrainingCommand(int code)
        {
            Code = code;
        }

        public TrainingCommand(int code, char type)
        {
            Code = code;
            Type = type;
        }
    }
}
