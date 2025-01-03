
using Workout.Domain.Emums;

namespace Workout.Domain.Entities.Session
{
    public class Session : Entity
    {
        public long Id { get; private set; }
        public int UserID { get; private set; }
        public AuthTypes AuthType { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime Expiration { get; private set; }
        public DateTime? Logout { get; private set; }
        public string Host { get; private set; }
        public string Key { get; private set; }
        public string UserAgent { get; private set; }
        public string UserIp { get; private set; }
        protected Session() { }
        public Session(int userID,
                       AuthTypes authType,
                       string accessToken,
                       string refreshToken,
                       DateTime expiration,
                       string sessionKey,
                       string userAgent,
                       string userIp,
                       string host)
        {
            UserID = userID;
            AuthType = authType;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Expiration = expiration;
            Key = sessionKey;
            UserAgent = userAgent;
            UserIp = userIp;
            Host = host;
        }

        public void DoLogout() => Logout = DateTime.Now;
    }
}
