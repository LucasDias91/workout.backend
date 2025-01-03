using AutoMapper;
using Workout.Api.Responses;
using Workout.Domain.Entities.Auth;
using Workout.Domain.Entities.Training;
using Workout.Domain.Entities.User;

namespace Workout.Api.AutoMapper
{
    public class DomainToResponseMappingProfile : Profile
    {
        public DomainToResponseMappingProfile()
        {
            CreateMap<Auth, SignInResponse>();
            CreateMap<User, ProfileResponse>();
            CreateMap<Timeline, TimelineResponse>();
        }
    }
}
