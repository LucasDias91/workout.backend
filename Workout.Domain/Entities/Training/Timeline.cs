


using System.ComponentModel.DataAnnotations.Schema;

namespace Workout.Domain.Entities.Training
{
    public class Timeline : Entity
    {
        public int UserId { get; private set; }
        public int Code { get; private set; }
        public char Type { get; private set; }
        public DateTime? Start { get; private set; }
        public DateTime? End { get; private set; }
        [NotMapped]
        public string Goal { get; private set; }
        [NotMapped]
        public string[] Groups { get; private set; }
        protected Timeline() { }
        public Timeline(int userId, int code, char type, DateTime? start, DateTime? end, string goal, IEnumerable<Training> items, IEnumerable<Timeline> timelines)
        {
            UserId = userId;
            Code = code;
            Type = type;
            Start = start;

            Goal = goal;
            Groups = items.Where(item=> item.Code == code)
                          .Select(item=> item.Group)
                          .Distinct()
                          .ToArray();
            Activate();
            Start = timelines.Where(item => item.Code == code).Min(item => item.Start);
            if(timelines.Where(item => item.Code == code).Count() > 4)
                End = timelines.Where(item => item.Code == code).Max(item=> item.End);
        }

        public Timeline(int userId, int code, char type)
        {
            UserId = userId;
            Code = code;
            Type = type;
            Start = DateTime.Now;
            Activate();
        }

        public void TimelineEnd() => End = DateTime.Now;
    }
}
