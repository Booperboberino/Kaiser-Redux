using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D referencePhysicalMap;
    public Tilemap worldPhysicalMap;
    public Tile landTile;
    public Tile waterTile;
    public Color mapWaterColor;
    public GameObject mapObject;
    public Texture2D writeTexture;

    void Start()
    {

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateLevel()
    {
        // writeTexture.Reinitialize(referencePhysicalMap.width,referencePhysicalMap.height);
        for (int x = 0; x < referencePhysicalMap.width; x++)
        {
            for (int y = 0; y < referencePhysicalMap.height; y++)
            {
                GenerateTile(x, y);
            }
        }
        mapObject.GetComponent<Renderer>().material.mainTexture = writeTexture;

    }
    void GenerateTile(int x, int y)
    {
        
        Color referencePhysicalMapPixelColor = referencePhysicalMap.GetPixel(x, y);

        Vector3Int currentPosition = new Vector3Int(x, y, 0);
        // Debug.Log(referencePhysicalMapPixelColor);
        // Debug.Log(Color.clear);
        // Debug.Log(referencePhysicalMapPixelColor.a == 0);
        // writeTexture.SetPixel(x,y, Color.red);
        // if (referencePhysicalMapPixelColor.a == 0) 
        // {
        //     writeTexture.SetPixel(x,y, Color.black);
        //     // worldPhysicalMap.SetTile(currentPosition, waterTile);

        // }
        // else
        // {
        //     writeTexture.SetPixel(x,y,Color.white);
        //     // worldPhysicalMap.SetTile(currentPosition, landTile);

        // }
        //         writeTexture.Apply();

        
    }
}
