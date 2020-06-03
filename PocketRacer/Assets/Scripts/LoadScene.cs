using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Object SceneToLoad;
    public int SceneIndexToLoad = -1;
    public bool LoadSceneOnStart = false;
    private bool sceneLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        if (LoadSceneOnStart)
            StartCoroutine(LoadTheSceneAsync());
    }

    private void OnEnable()
    {
        if (LoadSceneOnStart)
            StartCoroutine(LoadTheSceneAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadTheSceneAsync()
    {
        if (!sceneLoading)
        {
            sceneLoading = true;

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

            sceneLoading = false;

            //StartCoroutine(Gamekit3D.ScreenFader.FadeSceneIn());
        }
    }
}
