using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectInputChangScene : MonoBehaviour
{
    public Object SceneToLoad;
    public float SceneLoadAtStartDelay = -1f;
    public string SelectButtonInput = "Select";
    bool sceneLoaded = false;
    int currentSceneIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Gamekit3D.ScreenFader.FadeSceneOut(Gamekit3D.ScreenFader.FadeType.Loading));

        if (SceneLoadAtStartDelay > 0)
            Invoke("loadScene", SceneLoadAtStartDelay);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if(!sceneLoaded && (hinput.anyInput.pressed || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))//change scenes
        {
            if(Input.GetAxis(SelectButtonInput) != 0)
                StartCoroutine(LoadTheSceneAsync(currentSceneIndex - 1));
            else
                StartCoroutine(LoadTheSceneAsync(currentSceneIndex + 1));
        }
    }

    private void loadScene()
    {
        StartCoroutine(LoadTheSceneAsync(currentSceneIndex + 1));
    }

    IEnumerator LoadTheSceneAsync(int sceneToLoadIndex)
    {
        if (!sceneLoaded)
        {
            sceneLoaded = true;

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
            else if (sceneToLoadIndex >= 0)
                asyncLoad = SceneManager.LoadSceneAsync(sceneToLoadIndex);

            //yield return asyncLoad;

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone && asyncLoad.progress < .9f)
            {
                yield return null;
            }
        }

        //yield return StartCoroutine(Gamekit3D.ScreenFader.FadeSceneIn());
    }
}
