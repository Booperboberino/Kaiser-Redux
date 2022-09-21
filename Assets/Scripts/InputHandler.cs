using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    public SelectionManager selectionManager;
    public GridManager gridManager;

    public Tilemap worldTilemap;
    public Tile whiteTile;
    public Texture2D physicalMapTexture;
    public GameObject debugStick;


    private Vector3 selectionStart;
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

            // If we are NOT holding shift:
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                selectionManager.ClearSelection();
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (gridManager.GetCellFromPosition(selectionStart) != gridManager.GetCellFromPosition(currentWorldMousePosition))
            {
                IsDragging = true;
                selectionManager.UpdateSelectionBox(selectionStart, currentWorldMousePosition);
                
            }
        }
        // Mouse UP
        if(Input.GetMouseButtonUp(0))
        {
            // Get position
            Vector3 currentWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // If we ARE dragging:
            if(IsDragging)
            {
                // If we are NOT holding shift
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selectionManager.ClearSelection();
                    selectionManager.SelectTielsInSelectionBox(selectionStart, currentWorldMousePosition);
                }
                // If we ARE holding shift
                {
                    selectionManager.SelectTielsInSelectionBox(selectionStart, currentWorldMousePosition);

                }
                
                
            }
            
            //If we are NOT dragging:
            else
            {
                // If we are NOT holding shift:
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    // If selection is land on physical map
                    if (gridManager.isLand(currentWorldMousePosition))
                    {
                        // Select only one square
                        selectionManager.SelectOneSquare(gridManager.GetCellFromPosition(currentWorldMousePosition));

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
                    if (gridManager.isLand(currentWorldMousePosition))
                    {
                        // Add current tile to selection
                        selectionManager.AddToSelection(gridManager.GetCellFromPosition(currentWorldMousePosition));
                    }

                }

            }


        }


    }




}


