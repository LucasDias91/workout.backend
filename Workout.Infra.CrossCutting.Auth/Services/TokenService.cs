using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Workout.Infra.CrossCutting.Security.Configurations;
using Workout.Infra.CrossCutting.Security.DataModels;
using Workout.Infra.CrossCutting.Security.Extensions;
using Workout.Infra.CrossCutting.Security.Helpers;
using Workout.Infra.CrossCutting.Security.Services.Interfaces;

namespace Workout.Infra.CrossCutting.Security.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly SigningConfiguration _signingConfiguration;
        private readonly IDistributedCache _cache;

        public TokenService(TokenConfiguration tokenConfiguration,
                            IDistributedCache distributedCache)
        {
            _tokenConfiguration = tokenConfiguration;
            _signingConfiguration = new SigningConfiguration(tokenConfiguration.Secret);
            _cache = distributedCache;
        }

        public AccessDataModel CreateToken(string userName, int userId, string sessionKey)
        {
            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
            TimeSpan finalExpiration = TimeSpan.FromSeconds(_tokenConfiguration.FinalExpiration);

            var identity = JwtHelper.GetClaimsIdentity(userName, userId, sessionKey);

            var accessToken = JwtHelper.GenerateToken(_signingConfiguration.SigningCredentials,
                                                      identity,
                                                      creationDate,
                                                      expirationDate);

            var accessData = new AccessDataModel(creationDate, expirationDate, accessToken);

            var refreshTokenData = new RefreshTokenDataModel(accessData.RefreshToken, userId, sessionKey, userName);

            _cache.Add(accessData.RefreshToken, refreshTokenData, finalExpiration);

            return accessData;
        }

        public void Logout(string refreshToken) => _cache.Remove(refreshToken);
        public bool IsAuthorized(string refreshToken) => !string.IsNullOrEmpty(_cache.GetString(refreshToken));

    }
}
