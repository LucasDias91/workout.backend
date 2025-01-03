namespace Workout.Api.Responses
{
    public class TypeResponse
    {
        public char Code { get; set; }
        public char Type { get; set; }
        public string Group { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? Time { get; set; }
    }
}
