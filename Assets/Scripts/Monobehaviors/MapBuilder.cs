// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class MapBuilder : MonoBehaviour
// {
    
//     public GridManager gridManager;

//     public Texture2D referencePhysicalMap;
//     public Tilemap worldPhysicalMap;
//     public MapTile[,] mapTiles;


//     void Start()
//     {
//         mapTiles = new MapTile[referencePhysicalMap.width, referencePhysicalMap.height];
//         for (int x = 0; x < referencePhysicalMap.width; x++)
//         {
//             for (int y = 0; y < referencePhysicalMap.height; y++)
//             {
//                 mapTiles[x, y] = new MapTile();
//                 mapTiles[x, y].position = new Vector3Int(x, y);
//                 mapTiles[x, y].color = gridManager.GetColorFromCell(new Vector3Int(x, y));
//             }
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

    
// }
