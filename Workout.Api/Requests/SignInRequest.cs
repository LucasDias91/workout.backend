using Workout.Domain.Emums;

namespace Workout.Api.Requests
{
    public class SignInRequest
    {
        public AuthTypes AuthType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
    }
}
