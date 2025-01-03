using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Workout.Infra.CrossCutting.Exceptions;
using Workout.Infra.CrossCutting.Security.Services.Interfaces;

namespace Workout.Infra.CrossCutting.Security.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {

        public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {

            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            bool hasAllowAnonymous = filterContext.ActionDescriptor.EndpointMetadata
                                     .Any(em => em.GetType() == typeof(AllowAnonymousAttribute)); //< -- Here it is

            if (hasAllowAnonymous)
                return;


            var service = filterContext.HttpContext.RequestServices.GetService<ITokenService>();

            var token = filterContext.HttpContext.Request.Headers["Authorization"];

            var isAuthorized = service.IsAuthorized(token);

            if (!isAuthorized) return;

            throw new UnauthorizedException();
        }

    }
}
