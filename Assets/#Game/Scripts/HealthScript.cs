using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Private Variables")]
    [SerializeField]
    private int health = 1;

    [SerializeField]
    private bool isEnemy = true;

    private GameObject playerHealthIcon;
    public List<GameObject> playerHealthList = new List<GameObject>();

    public void Damage(int damageAmount)
    {

        if (gameObject.tag.Equals("Player") && health > 0)
        {
            Debug.Log(playerHealthList[health - 1]);
            GameObject temp = playerHealthList[health - 1];
            temp.SetActive(false);
            //playerHealthList.RemoveAt(health - 1);
            //Destroy(playerHealthList[health - 1], 1);
        }

        health -= damageAmount;

        if (health <= 0)
        {
            gameObject.GetComponent<FiringScript>().DestroyAmmo();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();
        if(shot != null)
        {
            if (shot.IsEnemyShot != isEnemy)
            {
            Damage(shot.DamageAmount());
            shot.gameObject.SetActive(false);
            }
        }

        HealthScript enemy = collision.gameObject.GetComponent<HealthScript>();
        if (enemy != null)
        {
            if (!enemy.IsEnemy())
            {
                Damage(1);
                enemy.Damage(1);
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

    public float Health()
    {
        return health;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

}
