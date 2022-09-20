using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    public SelectionManager selectionManager;
    public Tilemap worldMap;
    public Tile whiteTile;
    public Texture2D physicalMapTexture;
    public GameObject debugStick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Left click

        // Pressed down
        if (Input.GetMouseButtonDown(0))
        {
            // If we aren't holding shift
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                // Get clicked cell
                Vector3Int currentCell = worldMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                // Get color from cell
                Color clickedPhysicalColor = physicalMapTexture.GetPixel(currentCell.x + 1, currentCell.y + 1);
                Debug.Log(currentCell.x + ", " + currentCell.y);
                Debug.Log(clickedPhysicalColor);
                Instantiate(debugStick, new Vector3(currentCell.x + 1, 1, currentCell.y + 1), Quaternion.identity);
                // If selection is land on physical map
                if (clickedPhysicalColor.a != 0)
                {
                    selectionManager.SelectOneSquare(currentCell);
                    Debug.Log("Land!");
                }
                else
                {
                    Debug.Log("Water!");
                }
                // worldMap.SetTile(currentCell, whiteTile);
            }
        }

    }
}
