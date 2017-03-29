using System.Collections.Generic;
using UnityEngine;

namespace Common.Extensions
{
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
}
