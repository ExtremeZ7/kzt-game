using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Reflection;
using AssemblyCSharp;

//enums
public enum Tags
{
    [Description("Box Collider")]
    PlayerJumpCollider,

    [Description("Projectile")]
    PlayerElectricBolt,

    DynamicObjects}
;

public static class Help : System.Object
{
    public static bool UseAsTimer(ref float time)
    {
        time = Mathf.MoveTowards(time, 0f, Time.deltaTime);
        return Math.Abs(time) < float.Epsilon;
    }

    public static bool WaitForPlayer(ref PlayerControl playerControl)
    {
        if (playerControl != null)
            return true;
        playerControl = UnityEngine.Object.FindObjectOfType<PlayerControl>();
        return playerControl != null;
    }

    public static void forceRange(ref int value, int min, int max)
    {
        if (max >= min)
        {	
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
        }
        else
        {
            throw new ArgumentException("Max cannot be smaller than min", "min: " + min + " max:" + max);
        }
    }

    public static void forceRange(ref float value, float min, float max)
    {
        if (max >= min)
        {	
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
        }
        else
        {
            throw new ArgumentException("Max cannot be smaller than min", "min: " + min + " max:" + max);
        }
    }

    public static int forceRange(int value, int min, int max)
    {
        if (max >= min)
        {	
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
        }
        else
            throw new System.ArgumentException("Max cannot be smaller than min", "min: " + min + " max:" + max);
        return value;
    }

    public static float forceRange(float value, float min, float max)
    {
        if (max >= min)
        {	
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
        }
        else
            throw new System.ArgumentException("Max cannot be smaller than min", "min: " + min + " max:" + max);
        return value;
    }

    public static int IntMoveTowards(int current, int target, int speed)
    {
        if (current < target)
        {
            if (current + speed > target)
                return target;
            else
                return current + speed;
        }
        else if (current > target)
        {
            if (current - speed < target)
                return target;
            else
                return current - speed;
        }

        return current;
    }

    public static Color OffsetColor(Color currentColor, float rOffset, float gOffset, float bOffset)
    {
        return new Color((currentColor.r + rOffset) % 1.0f, (currentColor.g + gOffset) % 1.0f, (currentColor.b + bOffset) % 1.0f);
    }

    public static string groundTag = "Player On Ground";
    public static string airTag = "Player In Air";

    public static string GetLevelName(int world, int level)
    {
        string levelName = "";

        //Level Name
        TextAsset levelData = (TextAsset)Resources.Load("LevelData");
        XmlDocument levelNamesXml = new XmlDocument();
        levelNamesXml.LoadXml(levelData.text);

        XmlNode levelNode = levelNamesXml.SelectSingleNode("/levels/world[" + world + "]/level[" + level + "]");
        levelName = levelNode.Attributes["name"].Value;

        return levelName;
    }

    public static string GetLevelTag(int world, int level)
    {
        string levelTag = "";

        if (level > 0 && level <= 3)
            levelTag = "Level " + (level + ((world - 1) * 3)).ToString();
        else if (level == 4)
            levelTag = "High Voltage Challenge!";
        else if (level == 5)
            levelTag = "BOSS BATTLE";

        return levelTag;
    }

    public static string GetWorldName(int world)
    {
        string worldName = "";

        //Level Name
        TextAsset levelData = (TextAsset)Resources.Load("LevelData");
        XmlDocument levelNamesXml = new XmlDocument();
        levelNamesXml.LoadXml(levelData.text);

        XmlNode worldNode = levelNamesXml.SelectSingleNode("/levels/world[" + world + "]");
        worldName = worldNode.Attributes["name"].Value;

        return worldName;
    }

    public static string GetWorldDifficulty(int world)
    {
        string worldDiff = "";

        //Level Name
        TextAsset levelData = (TextAsset)Resources.Load("LevelData");
        XmlDocument levelNamesXml = new XmlDocument();
        levelNamesXml.LoadXml(levelData.text);

        XmlNode worldNode = levelNamesXml.SelectSingleNode("/levels/world[" + world + "]");
        worldDiff = worldNode.Attributes["difficulty"].Value;

        return worldDiff;
    }

    public static void GenerateHintBox(string message)
    {
        RemoveAnnoyingMessageBox();

        GameObject hintPrefab = Resources.Load("Prefabs/Helpful Hint", typeof(GameObject)) as GameObject;

        GameObject newHint = UnityEngine.Object.Instantiate(hintPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        drawHelpfulHint dhh = newHint.GetComponent<drawHelpfulHint>();
        dhh.stringToDraw = message;
    }

    public static void RemoveAnnoyingMessageBox()
    {
        drawHelpfulHint helperHint = GameObject.FindObjectOfType<drawHelpfulHint>();
        if (helperHint != null)
            UnityEngine.Object.Destroy(helperHint.gameObject);
    }

    public static Color ChangeColorAlpha(Color color, float newAlpha)
    {
        return new Color(color.r, color.g, color.b, newAlpha);
    }

    public static float FindMiddleY(Transform pointOne, Transform pointTwo)
    {
        float min = Mathf.Min(pointOne.position.y, pointTwo.position.y);
        float max = Mathf.Max(pointOne.position.y, pointTwo.position.y);
        return min + ((max - min) / 2);
    }

    public static Vector2 GetDividingPoint(Vector2 startPoint, Vector2 endPoint, int sections, int index)
    {
        return Vector3.MoveTowards(startPoint, endPoint, Vector3.Distance(startPoint, endPoint) / (float)sections * index);
    }
}

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
            rand = Math.Abs(rand);
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
            return data == start;
        }
    }

    public static bool IsNearZero(this float data)
    {
        return Math.Abs(data) < float.Epsilon;
    }
}

public static class RectExtensions
{
    public static Rect MoveDown(this Rect data, float distance, float newHeight)
    {
        return new Rect(data.x, 
            data.y + distance, 
            data.width, 
            data.height + newHeight);
    }
}