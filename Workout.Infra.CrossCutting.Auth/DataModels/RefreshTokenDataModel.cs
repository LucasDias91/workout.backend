namespace Workout.Infra.CrossCutting.Security.DataModels
{
    public class RefreshTokenDataModel
    {
        public string RefreshToken { get; private set; }
        public int UserId { get; private set; }
        public string SessionKey { get; private set; }
        public string UserName { get; private set; }
        protected RefreshTokenDataModel() { }

        public RefreshTokenDataModel(string refreshToken,
                                    int userId,
                                    string sessionKey,
                                    string userName)
        {
            RefreshToken = refreshToken;
            UserId = userId;
            SessionKey = sessionKey;
            UserName = userName;
        }
    }
}
