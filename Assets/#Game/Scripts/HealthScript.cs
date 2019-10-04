using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Universal Variables")]
    [SerializeField]
    private int health = 1;

    [Header("Enemy Variables")]
    [SerializeField]
    private bool isEnemy = true;

    [Header("Player Variables")]
    [SerializeField]
    Color flashColour;

    [SerializeField]
    Color normalColour;

    [SerializeField]
    int numFlashes = 3; 

    [SerializeField]
    private float iFrameSeconds = 2.0f;

    [SerializeField]
    private float flashSeconds = 0.08f;

    private bool iFrame = false;

    private int collisionDamage = 1;

    private SpriteRenderer playerSprite;

    private GameObject playerHealthIcon;
    private List<GameObject> playerHealthList = new List<GameObject>();

    private void Start()
    {
        if (gameObject.tag.Equals("Player"))
        {
            playerSprite = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    public void Damage(int damageAmount)
    {
        if (!iFrame)
        {
            health -= damageAmount;

            if (gameObject.tag.Equals("Player") && health > 0)
            {
                Debug.Log(playerHealthList[health]);
                GameObject temp = playerHealthList[health];
                temp.SetActive(false);
                StartCoroutine(IFrames());
            }
        }

        if (health <= 0)
        {
            gameObject.GetComponent<FiringScript>().DestroyAmmo();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();
        if(shot != null && !iFrame)
        {
            if (shot.IsEnemyShot != isEnemy)
            {
            Damage(shot.DamageAmount());
            shot.gameObject.SetActive(false);
            }
        }

        HealthScript enemy = collision.gameObject.GetComponent<HealthScript>();
        if (enemy != null && !iFrame)
        {
            if (!enemy.IsEnemy())
            {
                Damage(collisionDamage);
                enemy.Damage(collisionDamage);
            }
        }
    }

    public void InstansiatePlayerHealthBar()
    {
        if (gameObject.tag.Equals("Player"))
        {
            for (int index = 0; index < health; index++)
            {
                playerHealthIcon = Resources.Load<GameObject>("Prefab/PlayerLifePoint");
                float tempPos = -8;
                playerHealthIcon.GetComponent<Transform>().LeanSetPosX(tempPos+index);
                playerHealthIcon.SetActive(true);
                playerHealthList.Add(Instantiate(playerHealthIcon));
            }
        }
    }

    IEnumerator IFrames()
    {
        StartCoroutine(colourFlash());
        iFrame = true;
        yield return new WaitForSeconds(iFrameSeconds);
        iFrame = false;
    }

    IEnumerator colourFlash()
    {
        int temp = 0;
        if(playerSprite != null)
        {
            while (temp < numFlashes)
            {
                playerSprite.color = flashColour;
                yield return new WaitForSeconds(flashSeconds);
                playerSprite.color = normalColour;
                yield return new WaitForSeconds(flashSeconds);
                temp++;
            }
        }
        
    }

    public float Health()
    {
        return health;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

}
