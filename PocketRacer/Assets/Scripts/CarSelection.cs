using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public Transform MenuVehicle;
    public bool RotateSideways = false;
    public GameObject PrefabVehicle;
    public Object SceneToLoad;
    public int SceneIndexToLoad = -1;
    bool rotating = false;
    float rotationSpeed = 25f;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = MenuVehicle.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            if (RotateSideways)
                MenuVehicle.Rotate(Vector3.left * (rotationSpeed * Time.deltaTime));
            else
                MenuVehicle.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }

    void OnClick()
    {
        if (enabled)
        {
            SelectCar();
        }
    }

    void OnHover(bool isOver)
    {
        if (enabled)
        {
            //rotate model
            if (isOver)
                rotating = true;
            else
            {
                rotating = false;

                MenuVehicle.rotation = originalRotation;
            }
        }
    }

    public void SelectCar()
    {
        PlayerPrefs.SetString("CurrentVehicle", PrefabVehicle.name);

        Gamekit3D.ScreenFader.Instance.StopAllCoroutines();

        StartCoroutine(LoadTheSceneAsync());
    }

    IEnumerator LoadTheSceneAsync()
    {
        Gamekit3D.ScreenFader.Instance.StopAllCoroutines();

        StartCoroutine(Gamekit3D.ScreenFader.FadeSceneOut(Gamekit3D.ScreenFader.FadeType.Loading));

        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad.name);

        AsyncOperation asyncLoad = null;

        if (SceneToLoad != null)
            asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad.name);
        else if (SceneIndexToLoad >= 0)
            asyncLoad = SceneManager.LoadSceneAsync(SceneIndexToLoad);

        //yield return asyncLoad;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone && asyncLoad.progress < .9f)
        {
            yield return null;
        }

        //StartCoroutine(Gamekit3D.ScreenFader.FadeSceneIn());
    }
}
