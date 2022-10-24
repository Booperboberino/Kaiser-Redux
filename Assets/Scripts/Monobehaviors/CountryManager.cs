using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CountryManager : MonoBehaviour
{

    [Header("Managers:")]
    public GridManager gridManager;
    public PlayerManager playerManager;

    [Header("Other:")]
    public IDictionary<string, Country> countries;
    public List<MapTile> contestedTiles;

    // Start is called before the first frame update
    void Start()
    {

        // populate countries list
        // TODO: Read from .json file
        countries = new Dictionary<string, Country>();
        List<Country> unformattedCountres = new List<Country>()
        {
            new Country("Britain", Color.red, new Color32(255, 0, 0, 255)),
            new Country("France", Color.blue, new Color32(0, 0, 255, 255)),
        };
        foreach (Country country in unformattedCountres)
        {
            countries.Add(country.countryName, country);
        }
        unformattedCountres = null;


        GenerateCountriesFromMap();

    }

    public void AddTileToCurrentCountry(MapTile tile)
    {
        Country country = playerManager.playerCountry;
        Country tileCountry = gridManager.GetTile(tile.x, tile.y).ownedCountry;
        if (tileCountry != country)
        {
            country.ownedTiles.Add(tile);
            tile.countriesClaiming.Add(tileCountry);

            // if someone else already claims this tile, make it contested
            if (tileCountry != null)
            {
                contestedTiles.Add(tile);
                tile.isContested = true;
                tile.countriesClaiming.Add(country);
            }
        }
    }




    public void AddTileToCountryDebug(MapTile tile)
    {
        

        // if the country owning the tile isn't already the country being added
        if (gridManager.GetTile(tile.x, tile.y).ownedCountry != playerManager.playerCountry)
        {
            playerManager.playerCountry.ownedTiles.Add(tile);
        }
    }

    public void ReloadTileMapVisual()
    {
        foreach (KeyValuePair<string, Country> country in countries)
        {
            foreach (MapTile tile in country.Value.ownedTiles)
            {
                gridManager.WriteToFile(tile.x, tile.y, country.Value.dataMapColor);
                // gridManager.CreateCountryTile(new Vector3Int(tile.x, tile.y), country.Value.mapColor);
            }
        }
        
        gridManager.applyDataMapTexture(gridManager.countryDataMap);
    }

    public void GenerateCountriesFromMap()
    {
        // gridManager.countryDataMap.GetAllColors();
        // foreach(Color )
        // gridManager.countryDataMap.GetColor()
    }

    public Country getCountryFromColor(Color color)
    {
        foreach (KeyValuePair<string, Country> country in countries)
        {
            Debug.Log("Looking at country " + country.Key + ", with colour" + country.Value.mapColor + ", compared to color " + color);
            if (country.Value.dataMapColor == color)
            {
                Debug.Log("They're the same!");
                return country.Value;
            }
        }
        Debug.Log("Didn't find anything, returning null.");
        return null;
    }
    
}
