using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject levelOver;

    private TMPro.TextMeshPro[] levelOverText;

    private GameObject scoreGameObject;

    private TMPro.TextMeshPro scoreText;

    private MeshRenderer[] meshRenderers;

    private BoxCollider2D restartButton;

    private int score = 0;

    private static GameManager _instance;

    private static string sceneName = "SampleScene";


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

        scoreGameObject = GameObject.FindGameObjectWithTag("Score");
        scoreText = scoreGameObject.GetComponent<TMPro.TextMeshPro>();

    }

    void Start()
    {

        Player.Instance.transform.position = Vector3.zero;

        if (scoreText.text.Equals("SCORE"))
        {
            scoreText.SetText("0");
        }
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            if (hit.collider != null && hit.collider == restartButton)
            {
                Restart();
            }
        }
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
        HealthScript healthScript = Player.Instance.GetComponent<HealthScript>();
        healthScript.ResetPlayerHealth();
        healthScript.SetPlayerHealth(5);
        SceneManager.LoadScene(sceneName);
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.SetText(score.ToString());
    }

}
