using UnityEngine;

namespace Common.Math
{
    public static class Trigo
    {
        public static float PythagoreanOpposite(float angle, float hypotenuse)
        {
            return Mathf.Sin(angle * Mathf.Deg2Rad) * hypotenuse;
        }

        public static float PythagoreanAdjacent(float angle, float hypotenuse)
        {
            return Mathf.Cos(angle * Mathf.Deg2Rad) * hypotenuse;
        }

        public static Vector2 GetRotatedVector(float angle, float magnitude)
        {
            return new Vector2(PythagoreanAdjacent(angle, magnitude), PythagoreanOpposite(angle, magnitude));
        }

        public static float GetAngleBetweenPoints(Vector3 initialPosition, Vector3 targetPosition)
        {
            targetPosition.x = targetPosition.x - initialPosition.x;
            targetPosition.y = targetPosition.y - initialPosition.y;

            return Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        }
    }
}