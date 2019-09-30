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

        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if(enemy != null)
        {
            if (collision.gameObject.name == "Player")
            {
                Damage(1.0f);
                Destroy(enemy.gameObject);
            }
        }
    }

    public float Health()
    {
        return health;
    }

}
