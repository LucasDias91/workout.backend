using Workout.Infra.CrossCutting.Attributes.Validators.ModelState.Validator;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Workout.Infra.CrossCutting.Attributes.Validators.ModelState
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
                context.Result = new ValidationFailedResult(context.ModelState);
            base.OnActionExecuting(context);
        }
    }
}
