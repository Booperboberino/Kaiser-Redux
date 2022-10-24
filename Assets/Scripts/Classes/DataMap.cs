using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class DataMap
{

    public Texture2D dataMapTexture;

    public GameObject dataMapTextureObject;


    Texture2D tempTexture;


    public Color32[] pixels;


    public string dataMapName;
    public bool immutable;

    // File path for saving files
    public string appDataFilePath = Application.persistentDataPath + "/Temp/";




    public DataMap(GameObject textureObject, string dataMapName, bool immutable)
    {

        
        // Get map object
        this.dataMapTextureObject = textureObject;

        // Get map name, for file system use
        this.dataMapName = dataMapName;

        // Get texture of map
        this.dataMapTexture = textureObject.GetComponent<SpriteRenderer>().sprite.texture;


        
        tempTexture = new Texture2D(dataMapTexture.width, dataMapTexture.height);
        InitializeTempDataMapFile();
        tempTexture = GetTextureFromFile();
        pixels = tempTexture.GetPixels32();
        
            
    }

    private void InitializeTempDataMapFile()
    {
        if (!Directory.Exists(appDataFilePath))
        {
            Directory.CreateDirectory(appDataFilePath);
        }
        File.Delete(appDataFilePath + dataMapName + ".png");
        File.Copy( "Assets/Reference/" + dataMapName + ".png", appDataFilePath + dataMapName + ".png");
        Debug.Log("Copied file to " + appDataFilePath + dataMapName + ".png");

    }
    private Texture2D GetTextureFromFile()
    {
        byte[] byteArray = File.ReadAllBytes(appDataFilePath + dataMapName + ".png");
        Texture2D returnValue = new Texture2D(1, 1);
        returnValue.LoadImage(byteArray);
        return returnValue;
    }


    public Color GetColor(Vector3Int currentCell)
    {

        return pixels[(currentCell.x + 1) + (currentCell.y + 1) * dataMapTexture.width];

    }
    public Color GetColor(int x, int y)
    {
        return pixels[(x + 1) + (y + 1) * dataMapTexture.width];
    }
    public void SetPixel(int x, int y, Color writeColor)
    {
        tempTexture.SetPixel(x + 1, y + 1, writeColor);
        // Debug.Log("Set pixel at " + x + ", " + y + " to " + writeColor + " in temp texture, width " + tempTexture.width);
    }
    public void Apply()
    {
        byte[] pixelsBytes = tempTexture.EncodeToPNG();
        if (!Directory.Exists(appDataFilePath))
        {
            Directory.CreateDirectory(appDataFilePath);
        }
        // if the file already exists, replace it.
        if (File.Exists(appDataFilePath + dataMapName + ".png"))
        {
            File.Delete(appDataFilePath + dataMapName + ".png");
        }
        File.WriteAllBytes(appDataFilePath + dataMapName + ".png", pixelsBytes);
        tempTexture.filterMode = FilterMode.Point;
        tempTexture.Apply();
        Sprite returnSprite = Sprite.Create(tempTexture, new Rect(0, 0, tempTexture.width, tempTexture.height), new Vector2(0, 0), 1);
         
        
        dataMapTextureObject.GetComponent<SpriteRenderer>().sprite = returnSprite;
        Debug.Log("Applied changes to " + appDataFilePath + dataMapName + ".png. New texture size is " + tempTexture.width + "x" + tempTexture.height);
    }
}
