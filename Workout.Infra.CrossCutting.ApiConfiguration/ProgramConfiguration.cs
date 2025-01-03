using Microsoft.Extensions.DependencyInjection;
namespace Workout.Infra.CrossCutting.ApiConfiguration
{
    using Microsoft.Extensions.Logging;
    using Workout.Infra.CrossCutting.Validators.Class;
    using Workout.Infra.IoC;
    using System.Net.Mime;
    using Microsoft.AspNetCore.Builder;
    using Workout.Infra.CrossCutting.Logger;
    using Workout.Infra.CrossCutting.Extensions.Exeptions;
    using Microsoft.OpenApi.Models;
    using Serilog;
    using Workout.Infra.CrossCutting.Security;
    public static class ProgramConfiguration
    {
        public static void Configure<T>(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen()
                            .AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "[LsDias] - Workout Api", Version = "1" });
            }); 
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(T).Assembly));
            builder.Register(builder.Configuration);
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new ValidationFailedResult(context.ModelState);

                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        return result;
                    };
                });

            builder.Services.AddJwtSecurity(builder.Configuration);
        }
        public static void Run<T>(this WebApplicationBuilder builder)
        {
            var logger = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILogger<T>>();
            var config = builder.Configuration.ConfigSerilog();
    
            builder.Host.UseSerilog();
             
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseJwtSecutiry();
            app.UseCors("default");
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseGlobalExceptionHandler(config); ;
            app.MapControllers();
            app.Run();
        }
    }
}
