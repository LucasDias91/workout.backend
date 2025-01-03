using Workout.Domain.Core.Commands;
using Workout.Domain.Emums;

namespace Workout.Domain.Entities.Auth.Commands
{
    public class SignInCommand : Command<Auth>
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string? UserAgent { get; private set; }
        public string UserIp { get; private set; }
        public string Host { get; private set; }
        public AuthTypes AuthTypeId { get; private set; }
        public string RefreshToken { get; private set; }
        public SignInCommand(string userName,
                             string password,
                             string? userAgent,
                             string userIp,
                             string host,
                             AuthTypes authTypeId,
                             string refreshToken)
        {
            UserName = userName;
            Password = password;
            AuthTypeId = authTypeId;
            UserAgent = userAgent;
            UserIp = userIp;
            Host = host;
            RefreshToken = refreshToken;
        }
    }
}
