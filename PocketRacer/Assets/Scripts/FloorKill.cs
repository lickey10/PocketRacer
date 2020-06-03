using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CarClass;

public class FloorKill : MonoBehaviour
{
    public GameObject Explosion;
    public float CollisionPrefabScaleFactor = 2f;
    public AudioClip CollisionSound;
    public Object SceneToLoad;
    public int SceneIndexToLoad = 0; //default to loading screen
    public float GameOverScreenDisplayTime = 10f;
    public string SelectButtonInput = "Select";

    GameObject instantiatedCollisionPrefab;
    float lastInterval;
    bool sceneLoading = false;
    bool playerDied = false;
    //RaceManager raceManager = null;

    // Start is called before the first frame update
    void Start()
    {
        //raceManager = GameObject.FindGameObjectWithTag("RaceManager").GetComponent<RaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDied && (hinput.anyInput.pressed || Input.GetMouseButtonDown(0)))//change scenes
        {
            StartCoroutine(LoadTheSceneAsync());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.CompareTag("AI") || collision.transform.root.CompareTag("Player"))
        { 
            instantiatedCollisionPrefab = Instantiate(Explosion, collision.contacts[0].point, Quaternion.identity);
            instantiatedCollisionPrefab.transform.localScale = new Vector3(instantiatedCollisionPrefab.transform.localScale.x * CollisionPrefabScaleFactor, instantiatedCollisionPrefab.transform.localScale.y * CollisionPrefabScaleFactor, instantiatedCollisionPrefab.transform.localScale.z * CollisionPrefabScaleFactor);

            collision.transform.root.gameObject.GetComponent<Car>().DestroyVehicle(true);

            if (collision.transform.root.CompareTag("Player"))
            {
                playerDied = true;
                Invoke("showGameOver", 4);
            }
        }        
    }

    private void showGameOver()
    {
        StartCoroutine(LoadTheSceneAsync());
    }

    IEnumerator LoadTheSceneAsync()
    {
        if (!sceneLoading)
        {
            sceneLoading = true;

            Gamekit3D.ScreenFader.Instance.StopAllCoroutines();

            StartCoroutine(Gamekit3D.ScreenFader.FadeSceneOut(Gamekit3D.ScreenFader.FadeType.GameOver));

            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            float startTime = Time.realtimeSinceStartup;

            while (Time.realtimeSinceStartup < startTime + GameOverScreenDisplayTime)
            {
                //wait and allow game over to display for specified time
                yield return null;
            }

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
        }
    }
}
