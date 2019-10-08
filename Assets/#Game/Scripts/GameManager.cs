using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject scoreGameObject;

    private TMPro.TextMeshPro scoreText;

    private int score = 0;

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

    public void IncreaseScore()
    {
        score++;
        scoreText.SetText(score.ToString());
    }

}
