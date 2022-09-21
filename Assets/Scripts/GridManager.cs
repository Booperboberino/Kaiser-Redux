using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap worldTilemap;
    public Texture2D physicalMapTexture;
    public SelectionManager selectionManager;

    public Vector3Int GetCellFromPosition(Vector3 position)
    {
        return worldTilemap.WorldToCell(position);
    }

    public Color GetColorFromCell(Vector3Int currentCell)
    {
        return physicalMapTexture.GetPixel(currentCell.x + 1, currentCell.y + 1);
    }

    public Color GetColorFromPosition(Vector3 position)
    {
        return GetColorFromCell(GetCellFromPosition(position));
    }


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
        Debug.Log("Deleted tile");
    }
    public void DeleteTile(Vector3Int position)
    {
        worldTilemap.SetTile(position, null);
    }

    public List<Vector3Int> ReturnTilesInRange(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3Int startCell = GetCellFromPosition(startPosition);
        Vector3Int endCell = GetCellFromPosition(endPosition);
        List<Vector3Int> cellsInRange = new List<Vector3Int>();

        int deltaX = startCell.x-endCell.x;
        int deltaY = startCell.y-endCell.y;

        Debug.Log("Selecting range that is of size: " + deltaX + ", " + deltaY);
        Debug.Log("start " + startCell);
        Debug.Log("end: " + endCell);

        for (int currentYValue = 0; currentYValue < Mathf.Abs(deltaY); currentYValue++)
        {
            for (int currentXValue = 0; currentXValue < Mathf.Abs(deltaX); currentXValue++)
            {
            cellsInRange.Add(new Vector3Int(Mathf.Min(startCell.x,endCell.x) + currentXValue,Mathf.Min(startCell.y,endCell.y) + currentYValue));
            }
        }

        return cellsInRange;

    }

}
