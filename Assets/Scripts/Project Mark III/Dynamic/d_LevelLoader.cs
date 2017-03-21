//———————————————————————–
// <copyright file=”d_LevelLoader.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ColorToPrefab
{
    public enum TileClass
    {
        Terrain,
        Prop
    }

    public GameObject prefab;
    public Color32[] pixelMatrix;
    public TileClass tileClass;
}

// 'd' prefix means 'dynamic' which means it only runs once then usually
// stops or destroys itself afterwards
public class d_LevelLoader : MonoBehaviour
{
    public string levelFileName;
    public Transform terrainFolder;
    public Transform propsFolder;

    [Space(10)]
    public ColorToPrefab[] colorToPrefab;

    Dictionary<Color32[], ColorToPrefab> loadDict =
        new Dictionary<Color32[],ColorToPrefab>(new Color32EqualityComparer());
    
    [SerializeField]
    [Space(10)]
    bool mapLoaded;

    void Start()
    {
        if (mapLoaded)
        {
            return;
        }

        LoadMap();
    }

    void LoadAllLevelNames()
    {
        // Read the list of files from StreamingAssets/Levels/*.png
        // The player will progess through the levels alphabetically
    }

    [ContextMenu("Load Map")]
    void LoadMap()
    {
        EmptyMap();

        for (int i = 0; i < colorToPrefab.Length; i++)
        {
            loadDict.Add(colorToPrefab[i].pixelMatrix, colorToPrefab[i]);
        }
        Debug.Log(loadDict.ContainsKey(colorToPrefab[0].pixelMatrix));

        // Read the image data from the file in StreamingAssets
        string filePath = Application.dataPath + "/StreamingAssets/" +
                          levelFileName;
        byte[] bytes = System.IO.File.ReadAllBytes(filePath);
        var levelMap = new Texture2D(2, 2);
        levelMap.LoadImage(bytes);

        // Get the raw pixels from the level imagemap
        Color32[] allPixels = levelMap.GetPixels32();
        int width = levelMap.width;
        int height = levelMap.height;

        // Convert the pixel array into a 2D array
        var allPixels2D = new Color32[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                allPixels2D[x, y] = allPixels[(y * width) + x];
            }
        }

        // Spawn the tiles one by one
        for (int x = 0; x < width; x += 2)
        {
            for (int y = 0; y < height; y += 2)
            {
                Color32[] nextSquare =
                    {
                        allPixels2D[x, y],
                        allPixels2D[x + 1, y],
                        allPixels2D[x, y + 1],
                        allPixels2D[x + 1, y + 1]
                    };

                if (!SpawnTileAt(nextSquare, x / 2, y / 2))
                {
                    EmptyMap();
                    return;
                }
                //SpawnTileAt(allPixels[(y * width) + x], x, y);
            }
        }

        mapLoaded = true;
    }

    bool SpawnTileAt(Color32[] square, int x, int y)
    {
        // If this pixel is does not have full alphas then it's meant to just be
        // empty. Ignore it
        if (!IsFullAlpha(square))
        {
            return true;
        }

        // Find the right color in our map
        ColorToPrefab ctp;
        if (loadDict.TryGetValue(square, out ctp))
        {
            Transform newParent = null;
            switch (ctp.tileClass)
            {
                case ColorToPrefab.TileClass.Terrain:
                    newParent = terrainFolder;
                    break;

                case ColorToPrefab.TileClass.Prop:
                    newParent = propsFolder;
                    break;
            }

            // Spawn the prefab at the right location
            GameObject go = Instantiate(ctp.prefab, new Vector3(x, y, 0),
                                Quaternion.identity, newParent);
            PrefabUtility.ConnectGameObjectToPrefab(go, ctp.prefab);
        }
        else
        {
            // If we got to this point, it means we did not find a matching
            // color in our array.

            if (Debug.isDebugBuild)
            {
                Debug.LogError("No color to prefab found for combination at "
                    + x + " : " + y
                    + "{\n[0], " + square[0]
                    + "\n[1], " + square[1]
                    + "\n[2], " + square[2]
                    + "\n[3], " + square[3]
                    + "\n}");   
            }

            return false;
        }

        return true;
    }

    [ContextMenu("Empty Map")]
    void EmptyMap()
    {
        // Find all of our children and...eliminate them.
        mapLoaded = false;

        //Empty the dictionary as well
        loadDict = new Dictionary<Color32[], ColorToPrefab>();

        while (terrainFolder.childCount > 0)
        {
            Transform c = transform.GetChild(0);
            c.SetParent(null); // become Batman
            Destroy(c.gameObject); // become The Joker
        }

        while (propsFolder.childCount > 0)
        {
            Transform c = transform.GetChild(0);
            c.SetParent(null); // become Batman
            Destroy(c.gameObject); // become The Joker
        }
    }

    static bool IsFullAlpha(IList<Color32> square)
    {
        for (int i = 0; i < 4; i++)
        {
            if (square[i].a < 255)
            {
                /*if (Debug.isDebugBuild)
                {
                    Debug.Log("Color Square Ignored: " + square);
                }*/
                return false;
            }
        }
        return true;
    }


    void OnValidate()
    {
        for (int i = 0; i < colorToPrefab.Length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                colorToPrefab[i].pixelMatrix[j] = new Color32(
                    colorToPrefab[i].pixelMatrix[j].r,
                    colorToPrefab[i].pixelMatrix[j].g,
                    colorToPrefab[i].pixelMatrix[j].b,
                    255
                );
            }
        }
    }
}

public class Color32EqualityComparer : IEqualityComparer<Color32[]>
{
    public bool Equals(Color32[] x, Color32[] y)
    {
        if (x.Length != y.Length)
        {
            return false;
        }

        for (int i = 0; i < x.Length; i++)
        {
            if (!x[i].Equals(y[i]))
            {
                return false;
            }
        }
        return true;
    }

    public int GetHashCode(Color32[] obj)
    {
        return (int)Random.Range(0f, 10000f) + obj.Length;
    }
}
