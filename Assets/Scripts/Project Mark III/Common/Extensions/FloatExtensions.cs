using UnityEngine;

namespace Common.Extensions
{
    public static class FloatExtensions
    {
        public static float Variation(this float data, float range, bool absolute = false)
        {
            if (range.IsNearZero())
            {
                return data;
            }

            float rand = Random.Range(-range, range);
            if (absolute)
            {
                rand = Mathf.Abs(rand) * Mathf.Sign(range);
            }
            return data + rand;
        }

        public static float RestrictRange(this float data, float min, float max)
        {
            if (min >= max)
            {
                throw new UnityException("Parameter Problem With Restrict Range");
            }

            if (data < min)
            {
                return min;
            }
            else if (data > max)
            {
                return max;
            }

            return data;
        }

        public static float RestrictPositive(this float data)
        {
            return data < 0 ? 0 : data;
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
            return Mathf.Abs(data) < float.Epsilon;
        }

        public static bool IsNear(this float data, float other)
        {
            return Mathf.Abs(data - other) < float.Epsilon;
        }

        public static byte ToColorByte(this float data)
        {
            return (byte)((int)data * 255f);
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
    }
}
