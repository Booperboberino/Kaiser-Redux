using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    

    [Header("Map Textures")]
    public Texture2D physicalMapTexture;
    public Texture2D physicalMapTextureWest;
    public Texture2D physicalMapTextureEast;


    [Header("Managers:")]
    public SelectionManager selectionManager;

    [Header("Tilemaps:")]
    public Tilemap worldTilemap;
    public Tilemap countryTilemap;

    [Header("Tiles:")]

    public Tile countryTile;

    public DataMap physicalMap;

    void Start()
    {


        // Construct datamaps
        physicalMap = new DataMap(physicalMapTextureWest, physicalMapTextureEast);


    }


    public MapTile GetTile(int x, int y)
    {
        // MapTile generation, please keep chronological 
        bool isLand = physicalMap.GetColor(x, y).a != 0;
        Country ownedCountry = null;

        // return value
        return new MapTile(x, y, isLand, ownedCountry);
    }


    public Vector3Int GetCellFromPosition(Vector3 position)
    {
        return worldTilemap.WorldToCell(position);
    }

    public void CreateSelectionTile(Vector3Int position, Tile tile, Tilemap tilemap)
    {
        tilemap.SetTile(position, tile);
    }
    public void CreateSelectionTile(Vector3Int position, Tile tile)
    {
        worldTilemap.SetTile(position, tile);
    }

    
    public void CreateCountryTile(Vector3Int position, Color color)
    {
        countryTile.color = color;
        countryTilemap.SetTile(position, countryTile);
    }
   


    public void DeleteTile(Vector3Int position, Tilemap tilemap)
    {
        tilemap.SetTile(position, null);
    }
    public void DeleteTile(Vector3Int position)
    {
        // Default tilemap is selection tile map
        worldTilemap.SetTile(position, null);
    }

    public List<MapTile> ReturnTilesInRange(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3Int startCell = GetCellFromPosition(startPosition);
        Vector3Int endCell = GetCellFromPosition(endPosition);
        List<Vector3Int> cellsInRange = new List<Vector3Int>();
        List<MapTile> returnValue = new List<MapTile>();

        int deltaX = startCell.x - endCell.x;
        int deltaY = startCell.y - endCell.y;

        // Debug.Log("Selecting range that is of size: " + deltaX + ", " + deltaY);
        // Debug.Log("start " + startCell);
        // Debug.Log("end: " + endCell);

        for (int currentYValue = 0; currentYValue < Mathf.Abs(deltaY); currentYValue++)
        {
            for (int currentXValue = 0; currentXValue < Mathf.Abs(deltaX); currentXValue++)
            {
                cellsInRange.Add(new Vector3Int(Mathf.Min(startCell.x, endCell.x) + currentXValue, Mathf.Min(startCell.y, endCell.y) + currentYValue));
            }
        }
        foreach(Vector3Int cell in cellsInRange)
        {
            returnValue.Add(GetTile(cell.x, cell.y));
        }

        return returnValue;

    }

}
