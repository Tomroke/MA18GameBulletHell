using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed = new Vector2(50, 50);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;


    void Update()
    {
        float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        float inputY = CrossPlatformInputManager.GetAxis("Vertical");
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        if (CrossPlatformInputManager.GetButtonDown("FireingButton"))
        {
            FireingScript fireing = GetComponent<FireingScript>();
            if (fireing != null)
            {
                fireing.Attack(true);
            }
        }

    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }
}
