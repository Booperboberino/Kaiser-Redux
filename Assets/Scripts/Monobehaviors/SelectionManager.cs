using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager : MonoBehaviour
{

    public Tilemap worldTilemap;
    public GridManager gridManager;
    public Tile selectionTile;
    public UIManager uiManager;

    private Vector2 selectionSizeDelta;
    private Vector2 selectionAnchoredPosition;

    List<MapTile> selectedGridSquares = new List<MapTile>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SelectOneSquare(MapTile tile)
    {
        
        ClearSelection();
        selectedGridSquares.Add(tile);
        CreateSelectionVisual();
    }

    public void AddToSelection(MapTile tile)
    {
        selectedGridSquares.Add(tile);
        CreateSelectionVisual();
    }
    public void AddToSelection(List<MapTile> targetCells)
    {
        foreach (MapTile cell in targetCells)
        {

            if (cell.isLand)
            {
                selectedGridSquares.Add(cell);
            }
            
        }
        CreateSelectionVisual();
    }

    public void ClearSelection()
    {
        foreach(MapTile selected in selectedGridSquares)
        {
            gridManager.DeleteTile(new Vector3Int(selected.x, selected.y), worldTilemap);
        }
        selectedGridSquares.Clear();
    }

    public List<MapTile> GetSelectedSquares()
    {
        return selectedGridSquares;
    }

    public void CreateSelectionIndicator()
    {
        // TODO
    }

    public void CreateSelectionVisual()
    {
        foreach(MapTile selected in selectedGridSquares)
        {
            gridManager.CreateTile(new Vector3Int(selected.x, selected.y), selectionTile, worldTilemap);
        }
    }
    public void ClearSelectionVisual()
    {
        foreach(MapTile selected in selectedGridSquares)
        {
            gridManager.DeleteTile(new Vector3Int(selected.x, selected.y),worldTilemap);
        }
    }

    public void UpdateSelectionBox(Vector3 startPosition, Vector3 endPosition)
    {
        // find width of selection box
        float width = endPosition.x - startPosition.x;
        float height = endPosition.y - startPosition.y;
        

        // voodo shit, aka ui management
        selectionSizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionAnchoredPosition = new Vector2(startPosition.x, startPosition.y) + new Vector2(width / 2, height / 2);

        uiManager.UpdateSelectionBox(startPosition, endPosition);

        // uiManager.UpdateSelectionBox(new Vector2[] {selectionSizeDelta, selectionAnchoredPosition});
        // SelectSelectablesInBox();
    }
    public void SelectTilesInSelectionBox(Vector3 startPosition, Vector3 endPosition)
    {
        AddToSelection(gridManager.ReturnTilesInRange(startPosition,endPosition));
        
        // Remove UI selection box
        uiManager.clearSelectionBox();
    }
    


    // Unused
    // void SelectSelectablesInBox()
    //     {
    //         // ClearSelection();

    //         Vector2 min = selectionAnchoredPosition - (selectionSizeDelta / 2);
    //         Vector2 max = selectionAnchoredPosition + (selectionSizeDelta / 2);


    //         //refactor for tilemap
    //         foreach (Tile selectable in FindObjectsOfType<Tile>())
    //         {
    //             Vector3 screenPosition = Camera.main.WorldToScreenPoint(selectable.transform.position);

    //             if (screenPosition.x > min.x && screenPosition.x < max.x && screenPosition.y > min.y && screenPosition.y < max.y && !selectedGridSquares.Contains(selectable))
    //             {
    //                  AddToSelection(selectable);
    //             }
    //         }
    //     }
}
