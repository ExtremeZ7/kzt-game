using System.ComponentModel;
using System.Reflection;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<Enum>(this Enum data)
        {
            FieldInfo fi = data.GetType().GetField(data.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return data.ToString();
            }
        }
    }
}
