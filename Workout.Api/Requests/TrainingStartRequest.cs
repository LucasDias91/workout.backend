using System.ComponentModel.DataAnnotations;

namespace Workout.Api.Requests
{
    public class TrainingStartRequest
    {
        [Required]
        public int Code { get; set; }
        [Required]
        public char Type { get;  set; }
    }
}
