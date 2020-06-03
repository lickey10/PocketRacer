using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarClass;

public class DisplayPlaces : MonoBehaviour
{
    public Text[] PlacesText;
    RaceManager raceManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        raceManager = FindObjectOfType<RaceManager>();

        foreach (Car car in raceManager.allCars)
        {
            if (car != null && car.enabled)
                raceManager.carOrder[car.GetCarPosition(raceManager.allCars) - 1] = car;
        }

        //display results
        for (int x = 0; x < raceManager.carOrder.Length; x++)
        {
            if (PlacesText[x] != null && raceManager.carOrder[x] != null)
            {
                if (raceManager.carOrder[x].isActiveAndEnabled)
                    PlacesText[x].text = raceManager.carOrder[x].CarName;
                else
                    PlacesText[x].text = "DNF: " + raceManager.carOrder[x].CarName;
            }
        }
    }
}
