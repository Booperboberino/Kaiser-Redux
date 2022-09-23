using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    [Header("Managers:")]
    public SelectionManager selectionManager;
    public GridManager gridManager;
    public CountryManager countryManager;

    [Header("GameObjects:")]
    public GameObject debugStick;



    private Vector3 selectionStart;
    private Vector2 selectionStartScreenSpace;
    private bool IsDragging = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Left click

        // Mouse DOWN
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 currentWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectionStart = currentWorldMousePosition;
            selectionStartScreenSpace = Input.mousePosition;


            // If we are NOT holding shift:
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                selectionManager.ClearSelection();
            }


        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((selectionStartScreenSpace - new Vector2(Input.mousePosition.x, Input.mousePosition.y)).magnitude > 5)
            {
                IsDragging = true;
                selectionManager.UpdateSelectionBox(selectionStart, currentWorldMousePosition);
            }
        }
        // Mouse UP
        if (Input.GetMouseButtonUp(0))
        {


            // Get position in world
            Vector3 currentWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Get cell clicked on
            Vector3Int cell = gridManager.GetCellFromPosition(currentWorldMousePosition);
            // Get MapTile from clicked cell 
            MapTile clickedTile = gridManager.GetTile(cell.x, cell.y);

            // If we ARE dragging:
            if (IsDragging)
            {
                // If we are NOT holding shift
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selectionManager.ClearSelection();
                    selectionManager.SelectTilesInSelectionBox(selectionStart, currentWorldMousePosition);
                }
                // If we ARE holding shift
                {
                    selectionManager.SelectTilesInSelectionBox(selectionStart, currentWorldMousePosition);
                }
                IsDragging = false;

            }

            //If we are NOT dragging:
            else
            {


                // If we ARE holding SHIFT:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // If selection is land on physical map
                    if (clickedTile.isLand)
                    {
                        // Add current tile to selection
                        selectionManager.AddToSelection(clickedTile);
                    }
                }
                // If we ARE holding CONTROL:
                // DEBUG INTERACTION
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    countryManager.AddTileToCountryDebug(clickedTile);
                    Debug.Log(countryManager.countries["Britain"].ownedTiles.Count);
                }
                // If we are holding NOTHING:
                else
                {
                    // If selection is land on physical map
                    if (clickedTile.isLand)
                    {
                        selectionManager.SelectOneSquare(clickedTile);
                    }
                    // If selection is not land
                    else
                    {
                        selectionManager.ClearSelection();
                    }
                }
            }
        }
    }
}
