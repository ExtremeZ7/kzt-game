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
        new Dictionary<Color32[],ColorToPrefab>();
    
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

    [ContextMenu("Empty Map")]
    void EmptyMap()
    {
        // Find all of our children and...eliminate them.
        mapLoaded = false;

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

                SpawnTileAt(nextSquare, x / 2, y / 2);
                //SpawnTileAt(allPixels[(y * width) + x], x, y);
            }
        }

        mapLoaded = true;
    }

    void SpawnTileAt(Color32[] square, int x, int y)
    {

        // If this is a transparent pixel, then it's meant to just be empty.
        if (!IsFullAlpha(square))
        {
            return;
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

            Debug.LogError("No color to prefab found for combination: "
                + square);   
        }
    }

    static bool IsFullAlpha(IList<Color32> square)
    {
        for (int i = 0; i < 4; i++)
        {
            if (square[i].a < 255)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Color Square Ignored: " + square);
                }
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
