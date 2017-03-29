using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] SubArray<T>(this T[] data, int index, int length = 1)
        {
            var result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static T[] SubArrayDeepClone<T>(this T[] data, int index, int length = 1)
        {
            var arrCopy = new T[length];
            Array.Copy(data, index, arrCopy, 0, length);
            using (MemoryStream ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, arrCopy);
                ms.Position = 0;
                return (T[])bf.Deserialize(ms);
            }
        }

        public static int IndexOf<T>(this T[] data, T value)
        {
            int index = -1;
            foreach (T entry in data)
            {
                ++index;
                if (value.Equals(entry))
                    return index;
            }
            return -1;
        }

        public static bool Contains<T>(this T[] data, T value, bool returnFalseIfEmpty = false)
        {
            if (data.Length == 0)
                return !returnFalseIfEmpty;

            foreach (T element in data)
                if (element.Equals(value))
                    return true;
            return false;
        }

        public static List<string> ToStringList<T>(this T[] data)
        {
            Type enumType = typeof(T);
            //Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            List<string> enumValList = new List<string>(data.Length);
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                enumValList.Add(value.GetDescription());
            }

            return enumValList;
        }

        public static List<string> ToStringList<T>(this List<T> data)
        {
            List<string> enumValList = new List<string>(data.Count);
            foreach (T value in data)
            {
                enumValList.Add(value.GetDescription());
            }

            return enumValList;
        }

        public static List<T> ToList<T>(this T[] data)
        {
            List<T> valList = new List<T>(data.Length);
            foreach (T value in data)
            {
                valList.Add(value);
            }

            return  valList;
        }

        public static List<T> Resize<T>(this List<T> data, int size)
        {
            if (size < 0)
            {
                throw new ArgumentException("Size must not be negative!");
            }

            T[] listArray = data.ToArray(); 
            System.Array.Resize(ref listArray, size);
            return listArray.ToList();
        }
    }
}
