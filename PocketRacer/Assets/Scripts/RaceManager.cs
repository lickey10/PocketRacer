using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Utility;
using VehicleBehaviour.Utils;
using CarClass;

public class RaceManager : MonoBehaviour
{
    public int LapsPerRace = 3;
    public GameObject[] StartPositionsGOs;
    public WaypointCircuit[] AiCircuits;
    public Text[] PlacesText;
    public GameObject DefaultPlayerVehicle;
    public bool UseDefaultVehicle = false;
    [SerializeField] string selectInput = "Select";
    [SerializeField] string startInput = "Start";
    [HideInInspector]
    public Car[] allCars;
    [HideInInspector]
    public Car[] carOrder;
    public GameObject PauseMenu;
    public bool ThisIsADemoSceneNoPlayer = false;

    List<int> usedAIVehicles = new List<int>();
    List<int> usedStartPositions = new List<int>();
    GameObject[] aiVehicles;
    bool raceIsOver = false;
    bool restartingLevel = false;

    public void Start()
    {
        //instantiate players car
        string currentVehicleString = PlayerPrefs.GetString("CurrentVehicle", "");

        var currentVehicle = Resources.Load<GameObject>(currentVehicleString);

        //get AI vehicles
        aiVehicles = Resources.LoadAll<GameObject>("AIVehicles");
        List<GameObject> tempVehicleList = new List<GameObject>();

        for (int x = 0; x < StartPositionsGOs.Length; x++)
        {
            GameObject tempVehicle;
            int tempChosenIndex = -1;
            int whileLoopCount = 0;

            if (x == 0 && !ThisIsADemoSceneNoPlayer)
            {
                if (!UseDefaultVehicle && currentVehicle != null)
                    tempVehicle = GameObject.Instantiate(currentVehicle);
                else
                    tempVehicle = GameObject.Instantiate(DefaultPlayerVehicle);

                Camera.main.GetComponent<CameraFollow>().target = tempVehicle.transform;
            }
            else //choose random ai vehicle
            {
                tempVehicle = chooseRandomAIVehicle();
            }

            //choose random starting location
            GameObject tempStartPosition;
            tempChosenIndex = -1;
            whileLoopCount = 0;

            while ((usedStartPositions.Contains(tempChosenIndex) || tempChosenIndex == -1) && whileLoopCount < StartPositionsGOs.Length)
            {
                if (whileLoopCount < StartPositionsGOs.Length - 1)
                    tempChosenIndex = Random.Range(0, StartPositionsGOs.Length - 1);
                else //find last position available
                    for (int y = 0; y < StartPositionsGOs.Length; y++)
                    {
                        if (!usedStartPositions.Contains(y))
                        {
                            tempChosenIndex = y;

                            break;
                        }
                    }

                whileLoopCount++;
            }

            usedStartPositions.Add(tempChosenIndex);

            tempStartPosition = StartPositionsGOs[tempChosenIndex];

            tempVehicle.transform.position = tempStartPosition.transform.position;
            tempVehicle.transform.rotation = tempStartPosition.transform.rotation;

            tempVehicleList.Add(tempVehicle);
        }

        allCars = tempVehicleList.Select(x => x.GetComponent<Car>()).ToArray();

        // set up the car objects
        carOrder = new Car[allCars.Length];
        InvokeRepeating("ManualUpdate", 0.5f, 0.5f);
    }

    // this gets called every frame
    public void ManualUpdate()
    {
        foreach (Car car in allCars)
        {
            if (car != null && car.enabled)
                carOrder[car.GetCarPosition(allCars) - 1] = car;
        }

        //display results
        for (int x = 0; x < carOrder.Length; x++)
        {
            if (PlacesText[x] != null && carOrder[x] != null)
            {
                if (carOrder[x].isActiveAndEnabled)
                    PlacesText[x].text = carOrder[x].CarName;
                else
                    PlacesText[x].text = "DNF: " + carOrder[x].CarName;
            }
        }

        //stop car if LapsPerRace completed
        try
        {
            var cars = carOrder.Where(x => x != null && !x.Stopped && x.currentLap > LapsPerRace).ToList();

            if (cars.Count > 0)
                cars.ForEach(y => y.StopCar());

            //check if race is over
            cars = carOrder.Where(x => x != null && !x.Stopped && x.isActiveAndEnabled).ToList();

            if (cars.Count == 0)//the race is over
            {
                Debug.Log("Race is over");
                raceIsOver = true;

                StartCoroutine(Gamekit3D.ScreenFader.FadeSceneOut(Gamekit3D.ScreenFader.FadeType.GameOver));
            }
        }
        catch (System.Exception ex)
        {

            throw;
        }

    }

    private void FixedUpdate()
    {
        if (raceIsOver)
        {
            if (hinput.anyInput.pressed || hinput.anyGamepad.buttons.Any(x => x.pressed))
                RestartLevel();
        }
        else
        {
            if (GetInput(startInput) > 0.5 || GetInput(selectInput) > 0.5)//pause/unpause game
            {
                if (Time.timeScale == 1)
                {
                    PauseGame();

                    PauseMenu.SetActive(true);
                }
                else
                {
                    UnPauseGame();

                    PauseMenu.SetActive(false);
                }
            }
        }
    }

    public void RestartLevel()
    {
        if (!restartingLevel)
        {
            restartingLevel = true;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PauseGame()
    {
        StartCoroutine(PauseGameWithDelay(0));
    }

    public void PauseGame(float PauseDelay)
    {
        StartCoroutine(PauseGameWithDelay(PauseDelay));
    }

    private IEnumerator PauseGameWithDelay(float PauseDelay)
    {
        yield return new WaitForSeconds(PauseDelay);

        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    private float GetInput(string input)
    {
        return Input.GetAxis(input);
    }

    private GameObject chooseRandomAIVehicle()
    {
        GameObject returnVehicle = null;
        int tempChosenIndex = -1;
        int whileLoopCount = 0;
        //Random.InitState((int)Time.realtimeSinceStartup);

        while ((usedAIVehicles.Contains(tempChosenIndex) || tempChosenIndex == -1) && whileLoopCount < aiVehicles.Length * 2)
        {
            
            tempChosenIndex = Random.Range(0, aiVehicles.Length);
            whileLoopCount++;
        }

        returnVehicle = GameObject.Instantiate(aiVehicles[tempChosenIndex]);

        returnVehicle.GetComponent<WaypointProgressTracker>().circuit = AiCircuits;// AiCircuits[Random.Range(0, AiCircuits.Length - 1)];

        usedAIVehicles.Add(tempChosenIndex);

        return returnVehicle;
    }

    public void RespawnVehicle(GameObject Vehicle, float respawnDelay, Vector3 RespawnLocation)
    {
        foreach (GameObject ai in aiVehicles)
        {
            if (Vehicle.name.StartsWith(ai.name))
            {
                StartCoroutine(InstantiateVehicle(ai, respawnDelay, RespawnLocation));

                break;
            }
        }
    }

    private IEnumerator InstantiateVehicle(GameObject Vehicle, float delay, Vector3 location)
    {
        yield return new WaitForSeconds(delay);

        GameObject tempVehicle = GameObject.Instantiate(Vehicle);
        tempVehicle.transform.position = location;
        tempVehicle.GetComponent<WaypointProgressTracker>().circuit = AiCircuits;

        try
        {
            int vIndex = System.Array.FindIndex(allCars, w => w == null);
            allCars[System.Array.FindIndex(allCars, w => w == null)] = tempVehicle.GetComponent<Car>();
        }
        catch (System.Exception ex)
        {

            throw;
        }
        
    }
}
