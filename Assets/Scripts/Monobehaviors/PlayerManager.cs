using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [Header("Managers:")]
    public CountryManager countryManager;
    public UIManager uiManager;

    [Header("Other:")]
    public Country playerCountry;



    // Start is called before the first frame update
    void Start()
    {
        // debug setup
        playerCountry = countryManager.countries["England"];
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCountry(Country country)
    {
        playerCountry = country;
        Debug.Log("Set player country to " + country.countryName);
        uiManager.SetPlayerCountry(country.countryName);
    }

    

}
