using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMap
{

    public Texture2D dataMapTextureWest;
    public Texture2D dataMapTextureEast;

    public Color[] pixelsWest;
    public Color[] pixelsEast;

    public DataMap(Texture2D textureWest, Texture2D textureEast)
    {

        this.dataMapTextureWest = textureWest;
        this.dataMapTextureEast = textureEast;

        pixelsWest = dataMapTextureWest.GetPixels();
        pixelsEast = dataMapTextureEast.GetPixels();

    }

    public Color GetColor(Vector3Int currentCell)
    {
        Color returnValue;
        Debug.Log(currentCell);
        if (currentCell.x < dataMapTextureWest.width - 1)
        {
            returnValue = pixelsWest[(currentCell.x + 1) + (currentCell.y + 1) * dataMapTextureWest.width];
        }
        else
        {
            returnValue = pixelsEast[(currentCell.x + 1 - dataMapTextureEast.width) + (currentCell.y + 1) * dataMapTextureEast.width];
        }
        return returnValue;
    }
    public Color GetColor(int x, int y)
    {
        {
            Color returnValue;
            if (x < dataMapTextureWest.width - 1)
            {
                returnValue = pixelsWest[(x + 1) + (y + 1) * dataMapTextureWest.width];
            }
            else
            {
                returnValue = pixelsEast[(x + 1 - dataMapTextureEast.width) + (y + 1) * dataMapTextureEast.width];
            }


            return returnValue;
        }
    }

}
