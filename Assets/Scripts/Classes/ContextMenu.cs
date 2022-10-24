using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu 
{
   
    public Vector2 screenPosition;
    public Vector3 worldPosition;
    public GridManager gridManager;
    public Country clickedCountry;



    public ContextMenu(Vector2 screenPosition, Vector3 worldPosition, GridManager gridManager)
    {
        this.screenPosition  = screenPosition;
        this.worldPosition = worldPosition;
        this.gridManager = gridManager;

        this.clickedCountry = gridManager.GetTile(worldPosition).ownedCountry;

        
    }

}
