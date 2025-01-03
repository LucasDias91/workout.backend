using Workout.Infra.CrossCutting.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
namespace Workout.Infra.CrossCutting.Extensions.Exeptions
{
    public static class ExceptionHandlerExtension
    {
        private static string defaultErrMessage = "Desculpe! O sistema se comportou de forma inesperada, estamos trabalhando para melhorar os serviços!";
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var message = string.Empty;
                        var innerExeption = string.Empty;

                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                        logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";

                        switch (exceptionHandlerFeature.Error)
                        {
                            case KeyNotFoundException e:
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                message = defaultErrMessage;
                                break;
                            case InvalidCredentialException e:
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                message = e.Message;
                                if (e.InnerException != null)
                                    innerExeption = e.InnerException.ToString();
                                break;
                            case ApplicationNotFoundException e:
                                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                message = e.Message;
                                if (e.InnerException != null)
                                    innerExeption = e.InnerException.ToString();
                                break;

                            case UnauthorizedException e:
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                message = e.Message;
                                if (e.InnerException != null)
                                    innerExeption = e.InnerException.ToString();
                                break;
                            case ForbiddenException e:
                                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                message = e.Message;
                                if (e.InnerException != null)
                                    innerExeption = e.InnerException.ToString();
                                break;
                            case AppException e:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                message = e.Message;
                                if(e.InnerException != null)
                                    innerExeption = e.InnerException.ToString();
                                break;
                      
  
                            default:
                                // unhandled error
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                message = defaultErrMessage;
                                break;
                        }

#if DEBUG
                        throw new Exception(exceptionHandlerFeature.Error.Message, exceptionHandlerFeature.Error.InnerException);
#endif


                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Message = message }));
                    }
                });
            });
        }
    }
}
