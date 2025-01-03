using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Emums;
using Workout.Domain.Entities.Auth.Commands;
using Workout.Domain.Entities.Session.Repositories;
using Workout.Domain.Entities.User.Repositories;

namespace Workout.Service.Auth.Commands
{
    using System.Security.Authentication;
    using Workout.Domain.Entities.Auth;
    using Workout.Infra.CrossCutting.Security.Services.Interfaces;

    public class SignInCommandHandler : AuthBaseCommandHandler, IRequestHandler<SignInCommand, Auth>
    {
        public SignInCommandHandler(IUnitOfWork uow,
                                    IMediatorHandler mediator,
                                    INotificationHandler<DomainNotification> notifications,
                                    ISessionRepository sessionRepository,
                                    IUserRepository userService,
                                    IDistributedCache cache,
                                    ILogger<SignInCommandHandler> logger,
                                    ITokenService tokenService) : base(uow, mediator, notifications, userService, cache, logger, sessionRepository, tokenService)
        {
        }
        public async Task<Auth> Handle(SignInCommand request, CancellationToken cancellationToken)
        {

            switch (request.AuthTypeId)
            {
                case AuthTypes.Password:
                    return  await AuthByPasswordAsync(request.AuthTypeId,request.UserName, request.Password, request.UserAgent, request.UserIp, request.Host);
                case AuthTypes.RefreshToken:
                    return await AuthByRefreshTokenAsync(request.AuthTypeId,request.RefreshToken, request.UserAgent, request.UserIp, request.Host);
                default: throw new InvalidCredentialException();
            }
        }
    }
}
