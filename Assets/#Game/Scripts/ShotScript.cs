using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float animationSpeedY = 6.0f;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float animationSpeedX = 1.0F;

    [SerializeField]
    private float yDestination = -9;

    [SerializeField]
    private float damage = 1.0f;

    [SerializeField]
    private bool isEnemyShot = false;

    private int id;

    public void StartAnimation()
    {

        LeanTween.rotateAround(gameObject, Vector3.forward, 360, 6.0f).setLoopCount(10);
        id = LeanTween.moveY(gameObject, yDestination, animationSpeedY).id;
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
        LeanTween.cancel(id);
        gameObject.SetActive(false);
    }
}
