using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeToBlack;

    private float fadeSpeed = 3.0f;

    private SpriteRenderer spriteRendererController;

    private GameObject levelOver;

    private GameObject mainMenu;

    private TMPro.TextMeshPro[] levelOverText;

    private GameObject scoreGameObject;

    private TMPro.TextMeshPro scoreText;

    private BoxCollider2D restartButton;

    private BoxCollider2D[] menuButtons;

    private int score = 0;

    private int frames;

    private Scene currentScene;

    private string sceneName;

    private static GameManager _instance;


    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("Prefab/GameManager")).GetComponent<GameManager>();
            }

            return _instance;
        }
    }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        levelOver = GameObject.FindGameObjectWithTag("LevelOverScreen");
        if (levelOver != null)
        {
            restartButton = levelOver.GetComponentInChildren<BoxCollider2D>();
            levelOver.SetActive(false);
        }

        mainMenu = GameObject.FindGameObjectWithTag("UI");
        if(mainMenu != null)
        {
            menuButtons = mainMenu.GetComponentsInChildren<BoxCollider2D>();
        }

        if (fadeToBlack != null)
        {
            spriteRendererController = fadeToBlack.GetComponent<SpriteRenderer>();
        }

        scoreGameObject = GameObject.FindGameObjectWithTag("Score");
        scoreText = scoreGameObject.GetComponent<TMPro.TextMeshPro>();

    }

    void Start()
    {
        SceneManager.activeSceneChanged += HidePlayer;
        SceneManager.activeSceneChanged += HideFadeObject;
        SceneManager.activeSceneChanged += ChangeSceneWithEvent;
    }

    private void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (hit.collider != null)
                    {
                        if (hit.collider == restartButton)
                        {
                            sceneName = "MainMenu";
                            ChangeScene();
                        }

                        if (hit.collider == menuButtons[0])
                        {
                            sceneName = "Level1";
                            ChangeScene();
                        }

                        if (hit.collider == menuButtons[1])
                        {
                            Debug.Log(menuButtons[1]);
                        }

                        if (hit.collider == menuButtons[2])
                        {
                            Application.Quit();
                        }
                    }
                    break;
            }
        }
    }

    private void HidePlayer(Scene arg0, Scene arg1)
    {
        {
            currentScene = SceneManager.GetActiveScene();

            if (Player.Instance.gameObject.activeInHierarchy && currentScene.name.Equals("MainMenu") || currentScene.name.Equals("LoadScreen"))
            {
                Player.Instance.gameObject.SetActive(false);
                Player.Instance.gameObject.GetComponent<HealthScript>().HidePlayerHealth();
            }

            else if (!Player.Instance.gameObject.activeInHierarchy && !currentScene.name.Equals("MainMenu") && !currentScene.name.Equals("LoadScreen"))
            {
                Player.Instance.gameObject.SetActive(true);
                Player.Instance.gameObject.GetComponent<HealthScript>().ResetPlayerHealth();
                Player.Instance.transform.position = Vector3.zero;

                if (scoreText.text.Equals("SCORE"))
                {
                    scoreText.SetText("0");
                }
            }
        }
    }

    private void HideFadeObject(Scene arg0, Scene arg1)
    {
        fadeToBlack.SetActive(false);
    }

    public void ChangeSceneWithEvent(Scene arg0, Scene arg1)
    {
        ChangeScene();
    }

    public void ChangeScene()
    {
        if (sceneName != null && !SceneManager.GetActiveScene().name.Equals("level1"))
        {
            if (SceneManager.GetActiveScene().name == "LoadScreen")
            {
                Color color = spriteRendererController.color;
                color.a = 0;
                spriteRendererController.color = color;
                fadeToBlack.SetActive(true);
                LeanTween.alpha(fadeToBlack, 1.0f, fadeSpeed).setEaseInBack()
                    .setOnComplete(() => { SceneManager.LoadScene(sceneName, LoadSceneMode.Single); });
                //SceneManager.LoadScene("LoadScreen", LoadSceneMode.Single);
                //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            else
            {
                FadetoBlack();
            }
            
        }
        else
            {
                Debug.Log(sceneName);
            }
    }

    public void FadetoBlack()
    {
        Color color = spriteRendererController.color;
        color.a = 0;
        spriteRendererController.color = color;
        fadeToBlack.SetActive(true);
        LeanTween.alpha(fadeToBlack, 1.0f, fadeSpeed).setEaseInBack().setOnComplete(() => { SceneManager.LoadScene("LoadScreen", LoadSceneMode.Single); });
    }

    public void LevelOver()
    {
        levelOver.SetActive(true);
        levelOverText = levelOver.GetComponentsInChildren<TMPro.TextMeshPro>();
        levelOverText[0].SetText(SceneManager.GetActiveScene().name);
        levelOverText[1].SetText(score.ToString());
        StopAllCoroutines();
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.SetText(score.ToString());
    }
}
