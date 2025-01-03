namespace Workout.Api.Responses
{
    public class SignInResponse
    {
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
