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

    private static string sceneName = "Level1";


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

        HidePlayer();

    }

    void Start()
    {
        if (!currentScene.Equals("MainMenu"))
        {
            Player.Instance.transform.position = Vector3.zero;

            if (scoreText.text.Equals("SCORE"))
            {
                scoreText.SetText("0");
            }
        }
    }

    private void Update()
    {
        frames++;
        if (frames % 30 == 0)
        {
        //Debug.Log(frames);
            currentScene = SceneManager.GetActiveScene();
            //Debug.Log(currentScene.name);

            if (Player.Instance.gameObject.activeInHierarchy && currentScene.name.Equals("MainMenu") || currentScene.name.Equals("LoadScreen"))
            {
                HidePlayer();
            }

            else if (!Player.Instance.gameObject.activeInHierarchy && !currentScene.name.Equals("MainMenu") && !currentScene.name.Equals("LoadScreen"))
            {
                Player.Instance.gameObject.SetActive(true);
                Player.Instance.gameObject.GetComponent<HealthScript>().InstansiatePlayerHealthBar();
            }
        }

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
                            Restart();
                        }

                        if (hit.collider == menuButtons[0])
                        {
                            Debug.Log(menuButtons[0]);
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

    private void HidePlayer()
    {
        Player.Instance.gameObject.SetActive(false);
        Player.Instance.gameObject.GetComponent<HealthScript>().HidePlayerHealth();
    }

    public void LevelOver()
    {
        levelOver.SetActive(true);
        levelOverText = levelOver.GetComponentsInChildren<TMPro.TextMeshPro>();
        levelOverText[0].SetText("Level 1");
        levelOverText[1].SetText(score.ToString());
        StopAllCoroutines();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.SetText(score.ToString());
    }

}
