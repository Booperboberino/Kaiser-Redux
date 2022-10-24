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
    public UIManager uiManager;

    [Header("GameObjects:")]
    public GameObject debugStick;



    private Vector3 selectionStart;
    private Vector2 selectionStartScreenSpace;
    private bool IsDragging = false;
    private bool clickingOnUI = false;


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
            
            // if we ARE clicking on UI
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on UI");
            }
            // if we ARE NOT clicking on UI
            else
            {
                uiManager.ClearContextMenu();
                


                // If we are NOT holding shift:
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selectionManager.ClearSelection();
                }

            }
        }

        // Mouse HELD DOWN
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

            // If we ARE clicking on UI
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on UI");
            }

            // If we are NOT clicking on UI
            else
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
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        selectionManager.SelectTilesInSelectionBox(selectionStart, currentWorldMousePosition);

                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        foreach (MapTile tile in gridManager.ReturnTilesInRange(selectionStart, currentWorldMousePosition))
                        {
                            if (tile.isLand)
                            {
                                countryManager.AddTileToCountryDebug(tile);
                            }
                        }
                    }
                    // If we holding NOTHING
                    else
                    {
                        selectionManager.ClearSelection();
                        selectionManager.SelectTilesInSelectionBox(selectionStart, currentWorldMousePosition);

                    }
                    // Remove UI selection box
                    uiManager.ClearSelectionBox();
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

        // Right Click

        // Mouse UP

        if (Input.GetMouseButtonUp(1))
        {
            uiManager.CreateContextMenu(Input.mousePosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }


        // R key

        if (Input.GetKeyUp(KeyCode.R))
        {
            countryManager.ReloadTileMapVisual();
        }
    }
}
