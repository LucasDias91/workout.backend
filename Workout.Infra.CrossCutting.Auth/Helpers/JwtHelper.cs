using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Workout.Infra.CrossCutting.Security.Consts;

namespace Workout.Infra.CrossCutting.Security.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(SigningCredentials signingCredentials,
                                           ClaimsIdentity claims,
                                           DateTime CreationDate,
                                           DateTime ExpirationDate)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Params.SecurityKey)), SecurityAlgorithms.HmacSha256Signature),
                    Subject = claims,
                    NotBefore = CreationDate,
                    Expires = ExpirationDate
                });
                return handler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public static ClaimsIdentity GetClaimsIdentity(string userName,
                                                       int userId,
                                                       string sessionKey)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
           new GenericIdentity(userId.ToString(), "UserId"),
           new[] {
                    new Claim("UserId", userId.ToString()),
                    new Claim("SessionKey",  sessionKey),
                    new Claim("UserName", userName)
         }
        );

            return identity;
        }

        public static bool? ValidateJwtToken(string? token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Params.TokenKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
                return userId > 0;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
