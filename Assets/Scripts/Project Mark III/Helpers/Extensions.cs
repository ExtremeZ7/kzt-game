using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Reflection;

public static class ArrayExtensions
{
    public static T[] SubArray<T>(this T[] data, int index, int length = 1)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    public static T[] SubArrayDeepClone<T>(this T[] data, int index, int length = 1)
    {
        T[] arrCopy = new T[length];
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

    public static float SumTotal(this float[] data)
    {
        float result = 0f;
        foreach (float element in data)
        {
            result += element;
        }
        return result;
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

    public static string GetDescription<Enum>(this Enum data)
    {
        FieldInfo fi = data.GetType().GetField(data.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        else
        {
            return data.ToString();
        }
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
            enumValList.Add(GetDescription(value));
        }

        return enumValList;
    }

    public static List<string> ToStringList<T>(this List<T> data)
    {
        List<string> enumValList = new List<string>(data.Count);
        foreach (T value in data)
        {
            enumValList.Add(GetDescription(value));
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

public static class FloatExtensions
{
    public static float Variation(this float data, float range, bool absolute = false)
    {
        float rand = UnityEngine.Random.Range(-range, range);
        if (absolute)
        {
            rand = Math.Abs(rand) * Mathf.Sign(range);
        }
        return data + rand;
    }

    public static bool IsWithinRange(this float data, float start, float end)
    {
        //If the start of the range is less than the end, return true if the value is within the range.
        //If the start of the range is greater than the end, return true if the value is not within the range.
        //If the start of the range is equal to the end, return true if it is equal to start.
        if (start < end)
        {
            return data > start && data < end;
        }
        else if (start > end)
        {
            return data < start || data > end;
        }
        else
        {
            return data.IsNear(start);
        }
    }

    public static bool IsNearZero(this float data)
    {
        return Math.Abs(data) < float.Epsilon;
    }

    public static bool IsNear(this float data, float other)
    {
        return Math.Abs(data - other) < float.Epsilon;
    }

    public static byte ToColorByte(this float data)
    {
        return (byte)((int)data * 255f);
    }
}

public static class RectExtensions
{
    public static Rect MoveDown(this Rect data, float distance, float newHeight)
    {
        return new Rect(data.x, 
            data.y + distance, 
            data.width, 
            newHeight);
    }

    public static Rect SqueezeLeft(this Rect data, float squeezeSize)
    {
        return new Rect(data.x + squeezeSize,
            data.y,
            data.width - squeezeSize,
            data.height);
    }

    public static Rect SqueezeRight(this Rect data, float squeezeSize)
    {
        return new Rect(data.x,
            data.y,
            data.width - squeezeSize,
            data.height);
    }
}

public static class Vector2Extensions
{
    public static Vector2 Rotate(this Vector2 data, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        data.x = (cos * data.x) - (sin * data.y);
        data.y = (sin * data.x) + (cos * data.y);

        return data;
    }
}

public static class TransformExtensions
{
    public static Transform[] GetAllChildren(this Transform data,
                                             int limit = 0)
    {
        var children = new List<Transform>();
        bool noLimit = limit <= 0;

        for (int i = 0; i < data.childCount; i++)
        {
            children.Add(data.GetChild(i));

            if (!noLimit)
            {
                limit--;
                if (limit == 0)
                {
                    break;
                }
            }
        }

        return children.ToArray();
    }
}

public static class OtherExtensions
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

    public static string ToRGBAString(this Color32 data)
    {
        return "R:" + data.r
        + "G:" + data.g
        + "B:" + data.b
        + "A:" + data.a;
    }

    public static string ArrayToRGBAString(this Color32[] data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += data[i].ToRGBAString();
        }
        return result;
    }
}