using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMap 
{

    public Texture2D dataMapTexture;

    public Color[] pixels;
    public DataMap(Texture2D texture)
    {
        
        this.dataMapTexture = texture;

        pixels = dataMapTexture.GetPixels();
    }

    public Color GetColor(Vector3Int currentCell)
    {
        return pixels[(currentCell.x + 1) + (currentCell.y + 1) * dataMapTexture.width];
    }
    public Color GetColor(int x , int y)
    {

        return pixels[(x + 1) + (y + 1) * dataMapTexture.width];
    }

}
