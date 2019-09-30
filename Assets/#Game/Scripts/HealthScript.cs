using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Private Variables")]
    [SerializeField]
    private float health = 1.0f;

    [SerializeField]
    private bool isEnemy = true;

    public void Damage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0.0f)
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
        if (enemy != null && !enemy.IsEnemy())
        {
            Damage(1.0f);
            enemy.Damage(1.0f);
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
