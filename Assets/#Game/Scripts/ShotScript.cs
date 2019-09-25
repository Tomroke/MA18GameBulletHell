using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float damage = 1.0f;

    [SerializeField]
    private bool isEnemyShot = false;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 3.0f;

    [SerializeField]
    private LeanTweenType loopType;

    [SerializeField]
    private LeanTweenType easeType;

    private int id;

    public void StartAnimation(float x, float y)
    {
        id = LeanTween.move(gameObject, new Vector3(x, y,-0), speed)
                .setSpeed(speed)
                .setEase(easeType)
                .setLoopType(loopType)
                .id;
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
