﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private int damage = 1;
    private float speed = 3.0f;
    private Vector2 movementDirection;

    [SerializeField]
    private bool isEnemyShot = false;

    private void Update()
    {
        transform.Translate(movementDirection * speed *Time.deltaTime);
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

    public float SetSpeed
    {
        set { speed = value; }
    }
    public int SetDamage
    {
        set { damage = value; }
    }

    public int DamageAmount()
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
