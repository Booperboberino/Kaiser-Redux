using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataMap
{

    public Texture2D dataMapTextureWest;
    public Texture2D dataMapTextureEast;

    public Texture2D dataMapTexture;

    public GameObject dataMapTextureWestObject;
    public GameObject dataMapTextureEastObject;


    Texture2D tempTextureWest;
    Texture2D tempTextureEast;


    public Color32[] pixels;
    public Color[] pixelsWest;
    public Color[] pixelsEast;

    public string dataMapName;


    // File path for saving files
    public string appDataFilePath = Application.persistentDataPath + "/Temp/";




    public DataMap(GameObject textureWestObject, GameObject textureEastObject, Texture2D textureFull, string dataMapName)
    {
        
        // Get map object
        this.dataMapTextureWestObject = textureWestObject;
        this.dataMapTextureEastObject = textureEastObject;
        this.dataMapName = dataMapName;
        this.dataMapTexture = textureFull;

        // Get texture of map
        this.dataMapTextureEast = textureEastObject.GetComponent<SpriteRenderer>().sprite.texture;
        this.dataMapTextureWest = textureWestObject.GetComponent<SpriteRenderer>().sprite.texture;
        
        pixels = GetTextureFromFile().GetPixels32();

        pixelsWest = dataMapTextureWest.GetPixels();
        pixelsEast = dataMapTextureEast.GetPixels();


       

        // Test 
        InitializeTempDataMapFile();
        Debug.Log(GetTextureFromFile().width);




        // if (!Directory.Exists(appDataFilePath))
        // {
        //     Directory.CreateDirectory(appDataFilePath);
        // }
        // UnityEditor.FileUtil.DeleteFileOrDirectory(appDataFilePath + dataMapName + ".png");
        // FileUtil.CopyFileOrDirectory( "Assets/Reference/" + dataMapName + ".png", appDataFilePath + dataMapName + ".png");


        // byte[] byteArray = File.ReadAllBytes(appDataFilePath + dataMapName + ".png");
        // File.WriteAllBytes(appDataFilePath + "test2.png", byteArray);
        

        

        // Texture2D test = new Texture2D(1, 1);
        // test.LoadImage(byteArray);
        // File.WriteAllBytes(appDataFilePath + "test3.png", test.EncodeToPNG());

       


    }

    private void InitializeTempDataMapFile()
    {
        if (!Directory.Exists(appDataFilePath))
        {
            Directory.CreateDirectory(appDataFilePath);
        }
        UnityEditor.FileUtil.DeleteFileOrDirectory(appDataFilePath + dataMapName + ".png");
        FileUtil.CopyFileOrDirectory( "Assets/Reference/" + dataMapName + ".png", appDataFilePath + dataMapName + ".png");

    }
    private Texture2D GetTextureFromFile()
    {
        byte[] byteArray = File.ReadAllBytes(appDataFilePath + dataMapName + ".png");
        Texture2D test = new Texture2D(1, 1);
        test.LoadImage(byteArray);
        return test;
    }


    public Color GetColor(Vector3Int currentCell)
    {
        Color returnValue;
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
            tempTextureWest.SetPixel(x+1, y+1, writeColor);
        }
        else
        {
            Debug.Log("Writing color " + writeColor + " to pixel " + x + ", " + y + " in the east texture.");
            tempTextureWest.SetPixel(x+1, y-dataMapTextureWest.width+1, writeColor);
        }
    }
    public void Apply()
    {

       
       
       
       
       
       
       
       
       
        string pathWest = Application.persistentDataPath + "/Temp/" + tempTextureWest.name;
        File.WriteAllBytes(pathWest, tempTextureWest.EncodeToPNG());
        dataMapTextureWestObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tempTextureWest, new Rect(0, 0, tempTextureWest.width, tempTextureWest.height), new Vector2(0.5f, 0.5f), 100f);
 
        string pathEast = Application.persistentDataPath + "/Temp/" + dataMapTextureEast.name;
        File.WriteAllBytes(pathEast, dataMapTextureEast.EncodeToPNG());
        dataMapTextureWestObject.GetComponent<SpriteRenderer>().sprite.texture.Apply();
        dataMapTextureEast.Apply();
    }
}
