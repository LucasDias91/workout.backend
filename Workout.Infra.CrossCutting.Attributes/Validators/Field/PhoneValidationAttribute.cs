using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Workout.Infra.CrossCutting.Attributes.Validators.Field
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PhoneValidationAttribute : ValidationAttribute
    {
        private const string regex = "@\"^\\(\\d{2}\\)\\d{5}-\\d{4}$\"";

        public PhoneValidationAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
