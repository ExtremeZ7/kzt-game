using UnityEngine;

namespace Common.Extensions
{
    public static class ColorExtensions
    {
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
}