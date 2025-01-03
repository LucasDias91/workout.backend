using System.ComponentModel;

namespace Workout.Infra.CrossCutting.Extensions.Enums
{
    public static class EnumExtension
    {

        public static string ToDescription(this Enum enumType)
        {
            var memInfo = enumType.GetType().GetMember(enumType.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumType.ToString();
        }

    }
}
