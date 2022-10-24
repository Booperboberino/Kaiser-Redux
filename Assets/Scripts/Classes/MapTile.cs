using System.Collections.Generic;
public class MapTile 
{
    public int x;
    public int y;
    public bool isLand;
    public bool isContested = false;

    public List<Country> countriesClaiming = new List<Country>();
    public Country ownedCountry;

    public MapTile(int x, int y, bool isLand, Country ownedCountry)
    {
        this.x = x;
        this.y = y;
        this.isLand = isLand;
        this.ownedCountry = ownedCountry;
    }


}
