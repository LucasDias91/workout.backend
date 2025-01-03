using Workout.Api.AutoMapper.Config;
using Workout.Infra.CrossCutting.ApiConfiguration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureAutoMapper();
builder.Configure<Program>();
builder.Run<Program>();
