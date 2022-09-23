using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    public string countryName;
    public Color mapColor;
    public List<MapTile> ownedTiles;
    public List<MapTile> claimedTiles;
    

    public Country(string countryName, Color mapColor)
    {
        this.countryName = countryName;
        this.mapColor = mapColor;
        ownedTiles = new List<MapTile>();
        claimedTiles = new List<MapTile>();
    }

}
