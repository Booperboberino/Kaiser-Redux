using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    public SelectionManager selectionManager;
    public GridManager gridManager;
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
            if ((selectionStartScreenSpace - new Vector2(Input.mousePosition.x, Input.mousePosition.y)).magnitude > 40)
            {
                IsDragging = true;
                selectionManager.UpdateSelectionBox(selectionStart, currentWorldMousePosition);
            }
        }
        // Mouse UP
        if(Input.GetMouseButtonUp(0))
        {


            // Get position in world
            Vector3 currentWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Get cell clicked on
            Vector3Int cell = gridManager.GetCellFromPosition(currentWorldMousePosition);
            // Get MapTile from clicked cell 
            MapTile clickedTile = gridManager.GetTile(cell.x, cell.y);

            // If we ARE dragging:
            if(IsDragging)
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

                
                // If we are NOT holding shift:
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    // If selection is land on physical map
                    if (clickedTile.isLand)
                    {
                        // Select only one square
                        selectionManager.SelectOneSquare(clickedTile);
                    }
                    // If selection is not land
                    else
                    {
                        // Clear selection
                        selectionManager.ClearSelection();
                    }

                }
                // If we ARE holding shift:
                else
                {
                    // If selection is land on physical map
                    if (clickedTile.isLand)
                    {
                        // Add current tile to selection
                        selectionManager.AddToSelection(clickedTile);
                    }
                }
            }
        }
    }
}
