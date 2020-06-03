using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using VehicleBehaviour.Utils;

public class CameraManager : MonoBehaviour
{
    public Camera LogoCamera;
    public Camera[] AICameras;
    public float AICameraWatchTime = 0.5f;//the time to watch each AI camera
    public float LogoCameraWatchTime = 0.5f;//the time to watch logo camera
    public int NextSceneIndex;
    public int ThisSceneIndex = 1;

    private float nextCameraStartTime = 0.0f;
    private bool watchingLogo = true;
    private int currentAICameraIndex = -1;
    bool sceneLoaded = false;
    RaceManager raceManager;

    // Start is called before the first frame update
    void Start()
    {
        nextCameraStartTime = Time.time + LogoCameraWatchTime;

        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //assign cameras to cars
        raceManager = FindObjectOfType<RaceManager>();
        //int cameraIndex = 0;
        
        //foreach(Car car in raceManager.allCars)
        //{
        //    if(cameraIndex < AICameras.Length)
        //        AICameras[cameraIndex].GetComponent<CameraFollow>().target = car.transform;

        //    cameraIndex++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if ((hinput.anyGamepad.buttons.Any(x => x.pressed) || Input.GetMouseButtonDown(0)))//
        {
            StartCoroutine(LoadTheSceneAsync());
        }
        
        if (Time.time > nextCameraStartTime)
        {
            if(watchingLogo)//we were watching the logo camera and now need to cycle through the AI cameras
            {
                watchingLogo = false;
                LogoCamera.gameObject.SetActive(false);
            }

            currentAICameraIndex++;

            if (currentAICameraIndex < AICameras.Length)
            {
                if (raceManager != null && raceManager.allCars[currentAICameraIndex] != null)
                {
                    AICameras[currentAICameraIndex].GetComponent<CameraFollow>().target = raceManager.allCars[currentAICameraIndex].transform;
                }

                //skip this camera if it doesn't have a target
                if (AICameras[currentAICameraIndex].GetComponent<CameraFollow>().target != null)
                {
                    AICameras[currentAICameraIndex].gameObject.SetActive(true);

                    for (int x = 0; x < AICameras.Length; x++)//turn other cameras off
                    {
                        if (x != currentAICameraIndex)
                        {
                            AICameras[x].gameObject.SetActive(false);
                        }
                    }

                    nextCameraStartTime = Time.time + AICameraWatchTime;
                }
            }
            else if (!sceneLoaded) //we have gone through all the ai cameras - show logo
            {
                StartCoroutine(LoadTheSceneAsync());

                //watchingLogo = true;
                //LogoCamera.gameObject.SetActive(true);
                //currentAICameraIndex = -1;

                //nextCameraStartTime = Time.time + LogoCameraWatchTime;
            }            
        }
    }

    IEnumerator LoadTheSceneAsync()
    {
        sceneLoaded = true;

        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad;

        if (watchingLogo)//load next scene
        {
            Gamekit3D.ScreenFader.Instance.StopAllCoroutines();

            StartCoroutine(Gamekit3D.ScreenFader.FadeSceneOut(Gamekit3D.ScreenFader.FadeType.Loading));

            asyncLoad = SceneManager.LoadSceneAsync(NextSceneIndex);
        }
        else//reload this scene
            asyncLoad = SceneManager.LoadSceneAsync(ThisSceneIndex);

        //yield return asyncLoad;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone && asyncLoad.progress < .9f)
        {
            yield return null;
        }

        //yield return StartCoroutine(Gamekit3D.ScreenFader.FadeSceneIn());
    }
}
