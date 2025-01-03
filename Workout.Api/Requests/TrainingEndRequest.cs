using System.ComponentModel.DataAnnotations;

namespace Workout.Api.Requests
{
    public class TrainingEndRequest
    {
        [Required]
        public int Code { get; set; }
        [Required]
        public char Type { get;  set; }
    }
}
