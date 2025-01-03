using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Workout.Api.Responses
{
    public class ExerciseResponse
    {
        public string Exercise { get; set; }
        [JsonPropertyName("sxr")]
        public string SxR { get; set; }
        public string Technique { get; set; }
        public ExerciseResponse(string exercise, string sxr, string technique)
        {
            Exercise = exercise;
            SxR = sxr;
            Technique = technique;
        }
    }
}
