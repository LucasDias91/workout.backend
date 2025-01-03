using System.Linq;
using Workout.Api.Responses;
using Workout.Domain.Entities.Training;

namespace Workout.Api.AutoMapper.Extensions
{
    public static class AutoMapperExtensions
    {
        public static TrainingResponse? ToTrainingResponse(this IEnumerable<Training> items)
        {
            var plains = items.Select(item => new PlainResponse(item.Group, item.Type, items))
                              .ToList()
                              .DistinctBy(item=> item.Type);

            return items.Select(item => new TrainingResponse(item.Code, item.Goal, plains))
                        .ToList()
                        .Distinct()
                        .FirstOrDefault();
                  
        }

        public static PlainResponse? ToPlainResponse(this IEnumerable<Training> items)
        {
            var plains = items.Select(item => new PlainResponse(item.Group, item.Type, items))
                              .ToList()
                              .DistinctBy(item => item.Type);

            return plains.FirstOrDefault();

        }

    }
}
