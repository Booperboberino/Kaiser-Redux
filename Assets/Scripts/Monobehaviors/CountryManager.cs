using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryManager : MonoBehaviour
{

    public IDictionary<string, Country> countries;

    // Start is called before the first frame update
    void Start()
    {
        
        // populate countries list
        // TODO: Read from .json file
        countries = new Dictionary<string, Country>();
        // countries.Add(new Country("Germany", Color.gray));
        List<Country> unformattedCountres = new List<Country>()
        {
            new Country("Britain", Color.red)
        };
        foreach (Country country in unformattedCountres)
        {
            countries.Add(country.countryName, country);
        }
        unformattedCountres = null;


    }

    public void AddTileToCountry(MapTile tile, Country country)
    {
        country.ownedTiles.Add(tile);
    }


    public void AddTileToCountryDebug(MapTile tile)
    {
        countries["Britain"].ownedTiles.Add(tile);
    }
    

}
