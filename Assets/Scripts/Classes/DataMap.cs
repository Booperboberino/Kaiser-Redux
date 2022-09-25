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
    public void SetPixel(int x, int y, Color writeColor)
    {
        if (x < dataMapTextureWest.width - 1)
        {
            Debug.Log("Writing color " + writeColor + " to pixel " + x + ", " + y + " in the west texture.");
            dataMapTextureWest.SetPixel(x+1, y+1, writeColor);
        }
        else
        {
            Debug.Log("Writing color " + writeColor + " to pixel " + x + ", " + y + " in the east texture.");
            dataMapTextureWest.SetPixel(x+1, y-dataMapTextureWest.width+1, writeColor);
        }
    }
    public void Apply()
    {
        dataMapTextureWest.Apply();
        dataMapTextureEast.Apply();
    }
}
