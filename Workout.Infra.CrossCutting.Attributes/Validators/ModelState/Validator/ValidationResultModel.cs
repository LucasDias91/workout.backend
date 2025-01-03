using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Workout.Infra.CrossCutting.Attributes.Validators.ModelState.Validator
{
    public class ValidationResultModel
    {
        public string Message { get; }

            public List<ValidationError> Errors { get; }
            public bool ModelState { get; set; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            ModelState = true;
            Message = "A validação falhou!";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}
