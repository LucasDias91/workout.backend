using Workout.Domain.Core.Commands;

namespace Workout.Domain.Entities.Training.Commands
{
    using Workout.Domain.Emums;
    using Workout.Domain.Entities.Training;
    public class TrainingTimelineCommand : Command<List<Timeline?>>
    {
        public int UserId { get; private set; }
        public TimelineTypes Mode { get; private set; }
        protected TrainingTimelineCommand() { }
        public TrainingTimelineCommand(int userId, TimelineTypes mode)
        {
            UserId = userId;
            Mode = mode;
        }
    }
}
