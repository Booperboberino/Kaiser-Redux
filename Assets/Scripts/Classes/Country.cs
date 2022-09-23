using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    List<MapTile> ownedTiles;
    List<MapTile> claimedTiles;
    public string countryName;
    public Color mapColor;

    public Country(string countryName, Color mapColor)
    {
        this.countryName = countryName;
        this.mapColor = mapColor;
        ownedTiles = new List<MapTile>();
        claimedTiles = new List<MapTile>();
    }

}
