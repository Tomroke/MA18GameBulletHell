using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [Header("Private Variables")]
    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float damage = 1.0f;

    [SerializeField]
    private bool isEnemyShot = false;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 3.0f;

    private Vector2 movementDirection;

    private void Update()
    {
        transform.Translate(movementDirection * speed *Time.deltaTime);
        //transform.Rotate(Vector3.forward * 50 * Time.deltaTime, Space.World);
    }

    public void StartAnimation(Vector2 direction)
    {
        movementDirection = direction;
    }

    public bool IsEnemyShot
    {
        get { return isEnemyShot; }
        set { isEnemyShot = value; }
    }

    public float DamageAmount()
    {
        return damage;
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }

    }
}
