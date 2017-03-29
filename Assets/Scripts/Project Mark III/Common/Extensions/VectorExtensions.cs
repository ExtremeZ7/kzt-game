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

        public static Vector3 SetX(this Vector3 data, float newX)
        {
            return new Vector3(newX, data.y, data.z);
        }

        public static Vector3 SetY(this Vector3 data, float newY)
        {
            return new Vector3(data.x, newY, data.z);
        }

        public static Vector3 SetZ(this Vector3 data, float newZ)
        {
            return new Vector3(data.x, data.y, newZ);
        }

        public static Vector2 SetX(this Vector2 data, float newX)
        {
            return new Vector2(newX, data.y);
        }

        public static Vector2 SetY(this Vector2 data, float newY)
        {
            return new Vector2(data.x, newY);
        }
    }
}