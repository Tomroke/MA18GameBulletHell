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

    public GameObject player;

    private GameObject levelOver;

    private GameObject mainMenu;

    private TMPro.TextMeshPro[] levelOverText;

    private GameObject[] scoreGameObject;

    private TMPro.TextMeshPro[] scoreText;

    private BoxCollider2D restartButton;

    private BoxCollider2D[] menuButtons;

    private int score = 0;

    private Scene currentScene;

    private string sceneName;

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
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthScript>();
            player.SetActive(false);

        }

        levelOver = GameObject.FindGameObjectWithTag("LevelOverScreen");
        scoreGameObject = GameObject.FindGameObjectsWithTag("Score");
        scoreText = new TMPro.TextMeshPro[scoreGameObject.Length];
        if (levelOver != null)
        {
            if (scoreGameObject != null)
            {
                for (int index = 0; index < scoreGameObject.Length; index++)
                {
                    if (scoreText != null)
                    {
                        scoreText[index] = scoreGameObject[index].GetComponent<TMPro.TextMeshPro>();
                        scoreText[index].gameObject.SetActive(false);
                    }
                }
            

            restartButton = levelOver.GetComponentInChildren<BoxCollider2D>();
            levelOver.SetActive(false);
            }
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

        if (scoreText != null)
        {
            for (int index = 0; index < scoreText.Length; index++)
            {
                if (scoreText[index].text.Equals("SCORE"))
                {
                    scoreText[index].SetText(score.ToString());
                }
            }
        }

    }


    void Start()
    {
        SceneManager.activeSceneChanged += EndSceneChange;
        SceneManager.activeSceneChanged += GameScene;
    }


    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            Debug.Log("touch " + hit.collider);
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
                            Debug.Log("Menu " + menuButtons[0]);
                            StartSceneChange("Level 1");
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
        levelOver.SetActive(false);
        player.SetActive(false);
        scoreText[0].gameObject.SetActive(false);
        sceneName = scene;
        SceneManager.LoadScene("LoadScreen");
    }


    private void EndSceneChange(Scene sce1, Scene sce2)
    {
        if (SceneManager.GetActiveScene().name == "LoadScreen")
        {
            StartCoroutine(AsyncLoadScene(sceneName));
        }
    }


    IEnumerator AsyncLoadScene(string scene)
    {

        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(3.0f);
        async.allowSceneActivation = true;
        player.SetActive(true);
        scoreText[0].gameObject.SetActive(true);
    }


    private void GameScene(Scene sce1, Scene sce2)
    {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu") && !SceneManager.GetActiveScene().name.Equals("LoadScreen"))
        {
            player.GetComponent<Player>().InitializePlayerObjects();
        }
        else
        {
            player.SetActive(false);
        }
            
    }


    public void LevelOver()
    {
        levelOver.SetActive(true);
        levelOverText = levelOver.GetComponentsInChildren<TMPro.TextMeshPro>();
        levelOverText[1].SetText(SceneManager.GetActiveScene().name);
        levelOverText[0].SetText(score.ToString());
        StopAllCoroutines();
    }


    public void IncreaseScore()
    {
        score++;
        for (int index = 0; index < scoreGameObject.Length; index++)
        {
            scoreText[index].text = score.ToString();
        }
    }
}
