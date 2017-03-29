namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool Contains(this Tag[] data, string target)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Name == target)
                {
                    return true;
                }
            }
            return false;
        }

        public static X GetPropertyValue<T,X>(this T data, string propertyName)
        {              
            return (X)data.GetType().GetProperty(propertyName).GetValue(data, null);
        }
    }
}
