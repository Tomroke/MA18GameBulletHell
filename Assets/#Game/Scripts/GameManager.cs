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

    public GameObject levelOver;

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

    private GameObject player;
    private HealthScript playerHealth;

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
        //Singelton Code
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //Other code
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
            fadeToBlack.SetActive(false);
            spriteRendererController = fadeToBlack.GetComponent<SpriteRenderer>();
        }

        player = Resources.Load<GameObject>("Prefab/Player"); ;
        if(player != null)
        {
            Instantiate(player);
            playerHealth = player.GetComponent<HealthScript>();
            player.SetActive(false);
        }

        scoreGameObject = GameObject.FindGameObjectWithTag("Score");
        if (scoreGameObject != null)
        {
            scoreText = scoreGameObject.GetComponent<TMPro.TextMeshPro>();

            if (scoreText.text.Equals("SCORE"))
            {
                scoreText.SetText(score.ToString());
            }
        }

    }

    void Start()
    {
        SceneManager.activeSceneChanged += EndSceneChange;
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
                            StartSceneChange("MainMenu");
                        }

                        if (hit.collider == menuButtons[0])
                        {
                            StartSceneChange("Level1");
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


    public void StartSceneChange(string scene)
    {
        sceneName = scene;
        SceneManager.LoadScene("LoadScreen");
    }

    private void EndSceneChange(Scene sce1, Scene sce2)
    {
        if (SceneManager.GetActiveScene().name == "LoadScreen")
        {
            StartCoroutine(AsyncLoadScene(sceneName));
            player.SetActive(true);
        }
    }

    IEnumerator AsyncLoadScene(string scene)
    {

        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(3.0f);
        async.allowSceneActivation = true;
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
