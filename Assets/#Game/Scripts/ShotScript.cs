using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{

    [SerializeField]
    private float damage = 1.0f;

    [SerializeField]
    private bool isEnemyShot = false;

    void Update()
    {
        Destroy(gameObject, 15);
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
}
