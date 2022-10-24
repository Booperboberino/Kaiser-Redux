using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    public string countryName;
    public Color mapColor;
    public Color dataMapColor;
    public List<MapTile> ownedTiles;
    public List<MapTile> claimedTiles;

    public Country(string countryName, Color mapColor, Color dataMapColor)
    {
        this.countryName = countryName;
        this.mapColor = mapColor;
        this.dataMapColor = dataMapColor;
        ownedTiles = new List<MapTile>();
        claimedTiles = new List<MapTile>();
    }

}
