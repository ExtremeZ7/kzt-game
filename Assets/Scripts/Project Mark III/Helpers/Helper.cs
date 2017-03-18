using UnityEngine;
using System.Xml;
using System;

public static class Tags
{
    public static string dynamicObjects = "DynamicObjects";
}

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