namespace Workout.Domain.Entities.Auth
{
    using Workout.Domain.Entities.User;
    public class Auth
    {
        public DateTime Created { get; private set; }
        public DateTime Expiration { get; private set; }
        public string RefreshToken { get; private set; }
        public string AccessToken { get; private set; }
        public User User { get; private set; }
        protected Auth() { }
        public Auth(string created,
                    string expiration,
                    string refreshToken,
                    string accessToken)
        {
            Created = DateTime.Parse(created);
            Expiration = DateTime.Parse(expiration);
            RefreshToken = refreshToken;
            AccessToken = accessToken;
        }

        public Auth(User user)
        {
            User = user;
        }

        public static Auth Unauthorized(User user) => new Auth(user);
        public static Auth Unauthorized() => new Auth();
        public static Auth Authorized(string created,
                                      string expiration, 
                                      string refreshToken, 
                                      string accessToken) => new Auth(created,expiration, refreshToken, accessToken);
    }
}
