using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using Workout.Infra.CrossCutting.Security.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Workout.Infra.CrossCutting.Security.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Workout.Infra.CrossCutting.Security.Services.Interfaces;
using Workout.Infra.CrossCutting.Security.Services;

namespace Workout.Infra.CrossCutting.Security
{
    public static class SecurityModule
    {
        private const string MyAllowSpecificOrigins = "default";
        public static void AddJwtSecurity(this IServiceCollection services, IConfiguration configuration)
        {

            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            var signingConfigurations = new SigningConfiguration(tokenConfigurations.Secret);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;
                bearerOptions.IncludeErrorDetails = true;
                var paramsValidation = bearerOptions.TokenValidationParameters;
                var key = Encoding.UTF8.GetBytes(tokenConfigurations.Secret);

                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ClockSkew = TimeSpan.Zero;
                paramsValidation.ValidateIssuer = false;
                paramsValidation.ValidateAudience = false;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(key);
                bearerOptions.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        if (string.IsNullOrEmpty(context.Error))
                            context.Error = "invalid_token";
                        if (string.IsNullOrEmpty(context.ErrorDescription))
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";
                        // Add some extra context for expired tokens.
                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                            context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";
                        }



                        return context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            error = context.Error,
                            error_description = context.ErrorDescription
                        }));
                    },
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            services.AddCors(options =>
            {

                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                });
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(new AuthorizationFilter());
            });

            services.AddScoped<ITokenService, TokenService>();
        }
        public static void UseJwtSecutiry(this WebApplication app)
        {

            app.UseAuthentication();

        }

        public async static void Unauthorized(this HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Message = "Token expired!" }));
        }

        public static void Unauthorized(this AuthorizationFilterContext context)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

    }
}
