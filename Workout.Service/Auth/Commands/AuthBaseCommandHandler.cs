using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Workout.Domain.Core.Handlers;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Entities.Session.Repositories;
using Workout.Domain.Entities.User.Repositories;
using Workout.Infra.CrossCutting.Exceptions;
using Workout.Infra.CrossCutting.Security.Services.Interfaces;

namespace Workout.Service.Auth.Commands
{
    using Workout.Domain.Emums;
    using Workout.Domain.Entities.Auth;
    using Workout.Domain.Entities.Session;
    using Workout.Infra.CrossCutting.Security.DataModels;
    public abstract class AuthBaseCommandHandler : CommandHandler
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IDistributedCache _cache;
        protected readonly ILogger<AuthBaseCommandHandler> _logger;
        protected AuthBaseCommandHandler(IUnitOfWork uow,
                                         IMediatorHandler mediator,
                                         INotificationHandler<DomainNotification> notifications,
                                         IUserRepository userRepository,
                                         IDistributedCache cache,
                                         ILogger<AuthBaseCommandHandler> logger,
                                         ISessionRepository sessionRepository,
                                         ITokenService tokenService) : base(uow, mediator, notifications)
        {
            _cache = cache;
            _userRepository = userRepository;
            _logger = logger;
            _sessionRepository = sessionRepository;
            _tokenService = tokenService;
        }

        protected async Task<Auth> AuthByPasswordAsync(AuthTypes authType,
                                                      string userName, 
                                                      string password, 
                                                      string userAgent, 
                                                      string userIp, 
                                                      string host)
        {
            var user =  await Task.Run(()=> _userRepository.GetByCredentials(userName, password));

            if (user == null)
            {
                _logger.LogWarning("Usuário ou senha inválidos. login:{0}, password:{1}", userName, password);
                ErrorNotification("Usuário ou senha inválidos.");
                return Auth.Unauthorized();
            }
            user.EnsureIsActive();
            
            var accessData = Authorize(userName, user.Id, authType, userAgent, userIp, host);    

            return Auth.Authorized(accessData.Created, accessData.Expiration, accessData.RefreshToken, accessData.AccessToken);
        }

        protected async Task<Auth> AuthByRefreshTokenAsync(AuthTypes authType, string refreshToken, string userAgent, string userIp, string host)
        {
            RefreshTokenDataModel refreshTokenBase;
            string strTokenArmazenado = _cache.GetString(refreshToken);

            if (string.IsNullOrWhiteSpace(strTokenArmazenado))
                throw new UnauthorizedException();

            refreshTokenBase = JsonConvert.DeserializeObject<RefreshTokenDataModel>(strTokenArmazenado);

            if (refreshTokenBase == null)
                throw new UnauthorizedException();

            if (refreshToken != refreshTokenBase.RefreshToken)
                throw new UnauthorizedException();

            _cache.Remove(refreshToken);

            var accessData = Authorize(refreshTokenBase.UserName, refreshTokenBase.UserId, authType, userAgent, userIp, host);

            return Auth.Authorized(accessData.Created,accessData.Expiration, accessData.RefreshToken, accessData.AccessToken);
        }

        private AccessDataModel Authorize(string userName,
                                          int useId,
                                          AuthTypes authType,
                                          string userAgent,
                                          string userIp,
                                          string host)
        {
            string sessionKey = Guid.NewGuid().ToString();

            AccessDataModel accessData = _tokenService.CreateToken(userName, useId, sessionKey);
            var session = new Session(useId, authType, accessData.AccessToken, accessData.RefreshToken, DateTime.Parse(accessData.Expiration), sessionKey, userAgent, userIp, host);

            _sessionRepository.Add(session);
            _sessionRepository.Save();

            return accessData;
        }
    }
}
