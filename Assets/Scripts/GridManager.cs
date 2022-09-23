using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap worldTilemap;
    public Texture2D physicalMapTexture;
    public Texture2D physicalMapTextureWest;
    public Texture2D physicalMapTextureEast;
    public SelectionManager selectionManager;

    public Color[] pixelsWest;
    public Color[] pixelsEast;

    public DataMap physicalMap;

    void Start()
    {
        
        pixelsWest = physicalMapTextureWest.GetPixels();
        pixelsEast = physicalMapTextureEast.GetPixels();

        // Construct datamaps
        physicalMap = new DataMap(physicalMapTexture);

        
        



    }


    public MapTile GetTile(int x, int y)
    {
        // MapTile generation 

        bool isLand = physicalMap.GetColor(x, y).a != 0;
        return new MapTile(x, y, isLand);
    }


    public Vector3Int GetCellFromPosition(Vector3 position)
    {
        return worldTilemap.WorldToCell(position);
    }

    public Color GetColorFromCell(Vector3Int currentCell)
    {
        // Use during construction only
        Color returnValue;
        Debug.Log(currentCell);
        if (currentCell.x < physicalMapTextureWest.width-1)
        {
            returnValue = pixelsWest[(currentCell.x + 1) + (currentCell.y + 1) * physicalMapTextureWest.width];
            Debug.Log("Got color: " + returnValue + " at Western map");
        }
        else
        {
            returnValue = pixelsEast[(currentCell.x + 1 - physicalMapTextureWest.width) + (currentCell.y + 1) * physicalMapTextureEast.width];
            Debug.Log("Got color: " + returnValue + " at eastern map");
        }
        return returnValue;
    }
    public Color GetColorFromCell(int x, int y)
    {
        // Use during construction only
        Color returnValue;
        if (x < physicalMapTextureWest.width)
        {
            returnValue = pixelsWest[(x + 1) + (y + 1) * physicalMapTextureWest.width];
            Debug.Log("Got color: " + returnValue + " at Western map");

        }
        else
        {
            returnValue = pixelsEast[(x + 1 - physicalMapTextureWest.width) + (y + 1) * physicalMapTextureEast.width];
            Debug.Log("Got color: " + returnValue + " at eastern map");

        }
        return returnValue;
    }
    public Color GetColorFromPosition(Vector3 position)
    {
        return GetColorFromCell(GetCellFromPosition(position));
    }

    // TODO: remove
    public bool isLand(Vector3 position)
    {


        if (GetColorFromPosition(position).a != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool isLand(Vector3Int position)
    {
        if (GetColorFromCell(position).a != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CreateTile(Vector3Int position, Tile tile, Tilemap tilemap)
    {
        tilemap.SetTile(position, tile);
    }
    public void CreateTile(Vector3Int position, Tile tile)
    {
        worldTilemap.SetTile(position, tile);
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

    public List<Vector3Int> ReturnTilesInRange(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3Int startCell = GetCellFromPosition(startPosition);
        Vector3Int endCell = GetCellFromPosition(endPosition);
        List<Vector3Int> cellsInRange = new List<Vector3Int>();

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

        return cellsInRange;

    }

}
