using AutoMapper;

namespace Workout.Api.AutoMapper.Config
{
    public static class AutoMapperExtentions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var configMap = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToResponseMappingProfile>();
            });

            IMapper mapper = configMap.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
