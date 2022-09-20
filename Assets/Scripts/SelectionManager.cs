using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager : MonoBehaviour
{

    public Tilemap worldMap;

    List<Vector3Int> selectedGridSquares = new List<Vector3Int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SelectOneSquare(Vector3Int targetCell)
    {
        selectedGridSquares.Clear();
        selectedGridSquares.Add(targetCell);
    }

    public void AddToSelection(Vector3Int targetCell)
    {
        selectedGridSquares.Add(targetCell);
    }

    public void ClearSelection()
    {
        selectedGridSquares.Clear();
    }

    public List<Vector3Int> GetSelectedSquares()
    {
        return selectedGridSquares;
    }

    public void CreateSelectionIndicator()
    {
        // TODO
    }
}
