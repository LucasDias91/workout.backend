
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Workout.Infra.CrossCutting.Validators
{
    public class ValidateTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;

            const string reg = @"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";

            return Regex.Match(strValue, reg, RegexOptions.IgnoreCase).Success;

        }
    }
}
