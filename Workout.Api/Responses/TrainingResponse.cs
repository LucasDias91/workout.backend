namespace Workout.Api.Responses
{
    public class TrainingResponse
    {
        public int Code { get; set; }
        public string Goal { get; set; }
        public IEnumerable<PlainResponse> Plains { get;}

        public TrainingResponse(int code, string goal, IEnumerable<PlainResponse> plains)
        {
            Code = code;
            Goal = goal ;
            Plains = plains;
        }

    }
}
