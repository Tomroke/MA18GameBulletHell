using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed = new Vector2(50, 50);

    [SerializeField]
    private float collisionDamageAmount = 1.0f;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;


    void Update()
    {
        float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        float inputY = CrossPlatformInputManager.GetAxis("Vertical");
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        if (CrossPlatformInputManager.GetButtonDown("FireingButton"))
        {
            FiringScript fireing = GetComponent<FiringScript>();
            if (fireing != null)
            {
                fireing.Attack(false);
            }
        }

    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null)
                enemyHealth.Damage(enemyHealth.Health());

            damagePlayer = true;
        }

        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null)
                playerHealth.Damage(collisionDamageAmount);
        }

    }



}
