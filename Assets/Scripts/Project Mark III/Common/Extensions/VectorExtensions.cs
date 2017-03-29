using UnityEngine;

namespace Common.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 Rotate(this Vector2 data, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            data.x = (cos * data.x) - (sin * data.y);
            data.y = (sin * data.x) + (cos * data.y);

            return data;
        }

        public static Vector2 ToVector2(this Vector3 data)
        {
            return new Vector2(data.x, data.y);
        }
    }
}