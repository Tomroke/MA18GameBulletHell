using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    [SerializeField]
    private Vector2 speed = new Vector2(50, 50);

    [SerializeField]
    private Vector2 direction = new Vector2(0, -1);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    // Update is called once per frame
    void Update()
    {
            movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

}
