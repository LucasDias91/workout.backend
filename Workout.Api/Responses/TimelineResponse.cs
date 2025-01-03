namespace Workout.Api.Responses
{
    public class TimelineResponse
    {
        public int Code { get; set; }
        public string Goal { get; set; }
        public string[] Groups { get; set; }
        public DateTime? Start { get;  set; }
        public DateTime? End { get;  set; }
        public TimeSpan? Time { get; set; }
    }
}
