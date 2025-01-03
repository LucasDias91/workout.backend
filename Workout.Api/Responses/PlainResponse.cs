using Workout.Domain.Entities.Training;

namespace Workout.Api.Responses
{
    public class PlainResponse
    {
        public char Type { get; set; }
        public string[] Groups { get; set; }
        public IEnumerable<ExerciseResponse> Exercises { get; set; }

        public PlainResponse(string group,
                             char type, 
                             IEnumerable<Training> trainings)
        {
            Groups = group.Split(" + ");
            Type = type;
            Exercises = trainings.Where(item => item.Type == type)
                                 .Select(item => new ExerciseResponse(item.Exercise, item.SxR, item.Technique))
                                 .ToList()
                                 .Distinct(); ;
        }
    }
}
