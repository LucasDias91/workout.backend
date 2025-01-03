using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Workout.Infra.CrossCutting.Security.Extensions
{
    public static class IdentityExtensions
    {

        public static void Clear(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            claimsIdentity.Claims.ToList()
                         .ForEach(claim => claimsIdentity.RemoveClaim(claim))
           ;
        }

        public static bool ClaimExists(this IPrincipal principal, string claimType)
        {
            var ci = principal as ClaimsPrincipal;
            if (ci == null)
            {
                return false;
            }

            var claim = ci.Claims.FirstOrDefault(x => x.Type == claimType);

            return claim != null;
        }
        public static string GetIPAddress(this HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
            {
                return context.Request.Headers["X-Forwarded-For"];
            }
            return context.Connection?.RemoteIpAddress?.ToString();
        }

        public static int GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst("UserId");
            if (claim == null)
                return 0;

            return Convert.ToInt32(claim?.Value);

        }

        public static string GetSessionKey(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst("SessionKey");

            return claim?.Value;
        }
    }
}
