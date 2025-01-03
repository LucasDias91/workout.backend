using System.ComponentModel.DataAnnotations;

namespace Workout.Infra.CrossCutting.Validators
{
    public class ValidateDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime? date = value as DateTime?;

            if (date == null)
                return true;

            return date < DateTime.Now;
        }
    }
}