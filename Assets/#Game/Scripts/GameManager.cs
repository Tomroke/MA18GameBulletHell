using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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

        scoreGameObject = GameObject.FindGameObjectWithTag("Score");
        scoreText = scoreGameObject.GetComponent<TMPro.TextMeshPro>();

    }

    void Start()
    {
        SceneManager.activeSceneChanged += HidePlayer;
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
                            ChangeScene("MainMenu");
                        }

                        if (hit.collider == menuButtons[0])
                        {
                            ChangeScene("Level1");
                        }

                        if (hit.collider == menuButtons[1])
                        {
                            Debug.Log(menuButtons[1]);
                        }

                        if (hit.collider == menuButtons[2])
                        {
                            Debug.Log(menuButtons[2]);
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

    public void ChangeScene(string sceneName)
    {
        if(sceneName != null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log(sceneName);
        }
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
