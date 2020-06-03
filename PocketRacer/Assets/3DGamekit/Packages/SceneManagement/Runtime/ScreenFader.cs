using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gamekit3D
{
    public class ScreenFader : MonoBehaviour
    {
        public enum FadeType
        {
            Black, Loading, GameOver,
        }

        public static ScreenFader Instance
        {
            get
            {
                if (s_Instance != null)
                    return s_Instance;

                s_Instance = FindObjectOfType<ScreenFader>();

                if (s_Instance != null)
                    return s_Instance;

                Create();

                return s_Instance;
            }
        }

        public static bool IsFading
        {
            get { return Instance.m_IsFading; }
        }

        protected static ScreenFader s_Instance;

        public static void Create()
        {
            ScreenFader controllerPrefab = Resources.Load<ScreenFader>("ScreenFader");
            s_Instance = Instantiate(controllerPrefab);
        }


        public CanvasGroup faderCanvasGroup;
        public CanvasGroup loadingCanvasGroup;
        public CanvasGroup gameOverCanvasGroup;
        public float fadeDuration = 1f;
        public bool FadeOnStart = false;
        public float updateInterval = 0.1F;
        private float lastInterval;

        protected bool m_IsFading;
        int sceneCount = 0;

        const int k_MaxSortingLayer = 32767;

        void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            lastInterval = Time.realtimeSinceStartup;

            if (FadeOnStart)//for the loading screen at the beginning of the game
                FadeSceneOut(FadeType.Loading);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            Instance.StopAllCoroutines();

            StartCoroutine(FadeSceneIn());
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            sceneCount++;

            if (sceneCount > 1)
            {
                //Make sure another coroutine isn't trying to FadeSceneOut
                Instance.StopAllCoroutines();

                StartCoroutine(FadeSceneIn());
            }
        }

        protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup)
        {
            m_IsFading = true;
            canvasGroup.blocksRaycasts = true;
            float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
            while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
            {
                float timeNow = Time.realtimeSinceStartup;

                if (timeNow > lastInterval + updateInterval)
                {
                    //canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                    //               fadeSpeed * Time.deltaTime);

                    canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                                   fadeSpeed * (timeNow - lastInterval));

                    lastInterval = timeNow;
                }
               
                yield return null;
            }
            canvasGroup.alpha = finalAlpha;
            m_IsFading = false;
            canvasGroup.blocksRaycasts = false;
        }

        public static void SetAlpha(float alpha)
        {
            Instance.faderCanvasGroup.alpha = alpha;
        }

        public static IEnumerator FadeSceneIn()
        {
            CanvasGroup canvasGroup;
            if (Instance.faderCanvasGroup.alpha > 0.1f)
                canvasGroup = Instance.faderCanvasGroup;
            else if (Instance.gameOverCanvasGroup.alpha > 0.1f)
                canvasGroup = Instance.gameOverCanvasGroup;
            else
                canvasGroup = Instance.loadingCanvasGroup;

            yield return Instance.StartCoroutine(Instance.Fade(0f, canvasGroup));

            canvasGroup.gameObject.SetActive(false);
        }

        public static IEnumerator FadeSceneOut(FadeType fadeType = FadeType.Black)
        {
            CanvasGroup canvasGroup;
            switch (fadeType)
            {
                case FadeType.Black:
                    canvasGroup = Instance.faderCanvasGroup;
                    break;
                case FadeType.GameOver:
                    canvasGroup = Instance.gameOverCanvasGroup;
                    break;
                default:
                    canvasGroup = Instance.loadingCanvasGroup;
                    break;
            }

            canvasGroup.gameObject.SetActive(true);

            yield return Instance.StartCoroutine(Instance.Fade(1f, canvasGroup));
        }
    }
}