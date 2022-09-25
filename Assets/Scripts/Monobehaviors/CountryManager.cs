using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CountryManager : MonoBehaviour
{

    [Header("Managers:")]
    public GridManager gridManager;

    [Header("Other:")]
    public IDictionary<string, Country> countries;

    // Start is called before the first frame update
    void Start()
    {

        // populate countries list
        // TODO: Read from .json file
        countries = new Dictionary<string, Country>();
        List<Country> unformattedCountres = new List<Country>()
        {
            new Country("Britain", Color.red, new Color32(255, 0, 0, 255)),
        };
        foreach (Country country in unformattedCountres)
        {
            countries.Add(country.countryName, country);
        }
        unformattedCountres = null;


        GenerateCountriesFromMap();

    }

    public void AddTileToCountry(MapTile tile, Country country)
    {
        country.ownedTiles.Add(tile);
    }




    public void AddTileToCountryDebug(MapTile tile)
    {
        // if the tile isn't already tracked
        Debug.Log(!countries["Britain"].ownedTiles.Contains(tile));
        if (!countries["Britain"].ownedTiles.Contains(tile))
        {
            countries["Britain"].ownedTiles.Add(tile);
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
        gridManager.applyDataMapTexture();
    }

    public void GenerateCountriesFromMap()
    {
        // gridManager.countryDataMap.GetAllColors();
        // foreach(Color )
        // gridManager.countryDataMap.GetColor()
    }
}
