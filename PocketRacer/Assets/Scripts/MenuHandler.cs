using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    //public float PauseDelay = 0;
    //public bool RestartLevelOnDisable = false;
    public GameObject ContinueButton;
    public GameObject RestartButton;
    public GameObject ControllerButton;
    public GameObject CarsButton;
    public UILabel FirstPlace;
    public UILabel SecondPlace;
    public UILabel ThirdPlace;
    public UILabel FourthPlace;
    // <summary>
    /// Name of the axis used to send up and down key events.
    /// </summary>
    public string verticalAxisName = "Vertical";

    /// <summary>
    /// Name of the axis used to send left and right key events.
    /// </summary>
    public string horizontalAxisName = "Horizontal";

    public KeyCode selectButtonKeyCode = KeyCode.Return;
    [SerializeField] string selectInput = "Select";
    [SerializeField] string startInput = "Start";

    public RaceManager raceManager;
    GameObject selectedButton;
    bool sceneLoaded = false;
    List<GameObject> buttons = new List<GameObject>();
    int sceneIndexToLoad = -1;
    bool loadingPositions = false;

    // Used to ensure that joystick-based controls don't trigger that often
    static float mNextEvent = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //raceManager = GameObject.FindObjectOfType<RaceManager>();
        //raceManager.PauseGame(PauseDelay);

        buttons.Add(ContinueButton);
        buttons.Add(RestartButton);
        buttons.Add(ControllerButton);
        buttons.Add(CarsButton);

        //need to select the "continue" button
        selectedButton = ContinueButton;
        Notify(ContinueButton, "OnHover", true);
    }

    // Update is called once per frame
    void Update()
    {
        //watch for joystick input and select the buttons appropriatly
        if (Input.GetKeyUp(KeyCode.LeftArrow) || GetDirection(horizontalAxisName) != 0 || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (selectedButton == ContinueButton)
                selectedButton = RestartButton;
            else if (selectedButton == RestartButton)
                selectedButton = ContinueButton;
            else if (selectedButton == CarsButton)
                selectedButton = ControllerButton;
            else if (selectedButton == ControllerButton)
                selectedButton = CarsButton;

            //hover over selected button
            Notify(selectedButton, "OnHover", true);

            unHoverButtons();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || GetDirection(verticalAxisName) != 0 || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (selectedButton == ContinueButton)
                selectedButton = ControllerButton;
            else if (selectedButton == RestartButton)
                selectedButton = CarsButton;
            else if (selectedButton == CarsButton)
                selectedButton = RestartButton;
            else if (selectedButton == ControllerButton)
                selectedButton = ContinueButton;

            //hover over selected button
            Notify(selectedButton, "OnHover", true);

            unHoverButtons();
        }
        
        //watch for input and click the current button
        if (Input.GetKeyUp(selectButtonKeyCode) || hinput.anyGamepad.A.pressed || hinput.anyGamepad.B.pressed || hinput.anyGamepad.X.pressed || hinput.anyGamepad.Y.pressed)
        {
            Notify(selectedButton, "OnClick", true);
        }
    }

    private void OnEnable()
    {
        loadingPositions = false;
        FirstPlace.text = "";
        SecondPlace.text = "";
        ThirdPlace.text = "";
        FourthPlace.text = "";

        //display the car positions
        StartCoroutine("dislayPositionsAsync");

        raceManager.PauseGame();

        //need to select the "continue" button
        selectedButton = ContinueButton;
        //Notify(ContinueButton, "OnHover", true);
    }

    private void OnDisable()
    {
        //if (RestartLevelOnDisable)
        //    raceManager.RestartLevel();
        //else//just unpause
            //raceManager.UnPauseGame();
    }

    private void unHoverButtons()
    {
        //unhover over not selected buttons
        foreach (GameObject menuButton in buttons)
        {
            if (menuButton != selectedButton)
               Notify(menuButton, "OnHover", false);
        }
    }

    IEnumerator dislayPositionsAsync()
    {
        if (!loadingPositions)
        {
            loadingPositions = true;

            while (raceManager.carOrder == null || raceManager.carOrder.Length < 4 || raceManager.carOrder[3] == null)
            {
                fillPlacesText();

                yield return null;
            }

            loadingPositions = false;

            fillPlacesText();
        }

        yield return 0;
    }

    private void fillPlacesText()
    {
        if (FirstPlace.text.Length == 0 && raceManager.carOrder[0] != null)
            FirstPlace.text = raceManager.carOrder[0].name.Replace("(Clone)", "");

        if (SecondPlace.text.Length == 0 && raceManager.carOrder[1] != null)
            SecondPlace.text = raceManager.carOrder[1].name.Replace("(Clone)", "");

        if (ThirdPlace.text.Length == 0 && raceManager.carOrder[2] != null)
            ThirdPlace.text = raceManager.carOrder[2].name.Replace("(Clone)", "");

        if (FourthPlace.text.Length == 0 && raceManager.carOrder[3] != null)
            FourthPlace.text = raceManager.carOrder[3].name.Replace("(Clone)", "");
    }

    public void ClickContinue()
    {
        raceManager.UnPauseGame();
        transform.root.gameObject.SetActive(false);
    }

    public void ClickRestart()
    {
        sceneIndexToLoad = 4;
        StartCoroutine(LoadTheSceneAsync());
    }

    public void ClickCars()
    {
        sceneIndexToLoad = 2;
        StartCoroutine(LoadTheSceneAsync());
    }

    public void ClickController()
    {
        sceneIndexToLoad = 3;
        StartCoroutine(LoadTheSceneAsync());
    }

    /// <summary>
    /// Generic notification function. Used in place of SendMessage to shorten the code and allow for more than one receiver.
    /// </summary>

    static public void Notify(GameObject go, string funcName, object obj)
    {
        if (go != null)
        {
            go.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// Using the joystick to move the UI results in 1 or -1 if the threshold has been passed, mimicking up/down keys.
    /// </summary>

    static int GetDirection(string axis)
    {
        float time = Time.realtimeSinceStartup;

        if (mNextEvent < time)
        {
            float val = Input.GetAxis(axis);

            if (val > 0.75f)
            {
                mNextEvent = time + 0.25f;
                return 1;
            }

            if (val < -0.75f)
            {
                mNextEvent = time + 0.25f;
                return -1;
            }
        }
        return 0;
    }

    IEnumerator LoadTheSceneAsync()
    {
        sceneLoaded = true;

        Gamekit3D.ScreenFader.Instance.StopAllCoroutines();

        StartCoroutine(Gamekit3D.ScreenFader.FadeSceneOut(Gamekit3D.ScreenFader.FadeType.Loading));

        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad.name);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndexToLoad);

        //yield return asyncLoad;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone && asyncLoad.progress < .9f)
        {
            yield return null;
        }

        transform.root.gameObject.SetActive(false);
        raceManager.UnPauseGame();

        //yield return StartCoroutine(Gamekit3D.ScreenFader.FadeSceneIn());

    }
}
