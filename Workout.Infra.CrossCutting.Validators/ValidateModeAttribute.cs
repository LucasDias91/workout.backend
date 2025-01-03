using System.ComponentModel.DataAnnotations;

namespace Workout.Infra.CrossCutting.Validators
{
    public class ValidateModeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;
            string[] modes = { "web", "mobile" };

            if (string.IsNullOrEmpty(strValue))
                return true;

            return modes.Contains(strValue.ToLower());
        }
    }
}