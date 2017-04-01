using UnityEngine;

namespace Common.Extensions
{
    public static class RectExtensions
    {
        public static Rect MovePos(this Rect data, Vector2 distance)
        {
            return new Rect(data.x + distance.x,
                data.y + distance.y,
                data.width,
                data.height);
        }

        public static Rect AddToY(this Rect data, float distance)
        {
            return new Rect(data.x, 
                data.y + distance, 
                data.width, 
                data.height);
        }

        public static Rect AddToX(this Rect data, float distance)
        {
            return new Rect(data.x + distance, 
                data.y,
                data.width, 
                data.height);
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

        public static Rect SqueezeDown(this Rect data, float squeezeSize)
        {
            return new Rect(data.x,
                data.y,
                data.width,
                data.height - squeezeSize);
        }

        public static Rect SqueezeUp(this Rect data, float squeezeSize)
        {
            return new Rect(data.x,
                data.y + squeezeSize,
                data.width,
                data.height - squeezeSize);
        }

        public static Rect SetWidth(this Rect data, float newWidth)
        {
            return new Rect(data.x,
                data.y,
                newWidth,
                data.height);
        }

        public static Rect SetHeight(this Rect data, float newHeight)
        {
            return new Rect(data.x,
                data.y,
                data.width,
                newHeight);
        }
    }
}