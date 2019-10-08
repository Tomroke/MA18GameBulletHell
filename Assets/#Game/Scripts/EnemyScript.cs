using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private FiringScript firing;

    private bool inCameraView = false;

    [Header("Bullet Variables")]
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float rateOfFire = 0.5f;

    [Range(1, 100)]
    [SerializeField]
    private int bulletsPerShot;

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float coolDownPerSec = 0.25f;

    [Range(1, 50)]
    [SerializeField]
    private int ammoAmount = 10;

    [SerializeField]
    [Range(1, 50)]
    private int bulletDamage = 1;

    [SerializeField]
    [Range(1, 50)]
    private float bulletSpeed = 3.0f;

    [Range(0, 360)]
    [SerializeField]
    private float startAngle;

    [Range(0, 360)]
    [SerializeField]
    private float endAngle;

    [SerializeField]
    private Sprite bulletSprite;

    [SerializeField]
    private float spriteScale;

    void Awake()
    {

        firing = GetComponent<FiringScript>();
        firing.SetFireRules(rateOfFire, coolDownPerSec, bulletsPerShot, ammoAmount, true, startAngle, endAngle, bulletSprite, bulletSpeed, bulletDamage, spriteScale);
    }

    void Update()
    {
        if (firing != null && firing.CanAttack && inCameraView)
        {
            firing.Attack();
        }
    }

    private void OnBecameVisible()
    {
        inCameraView = true;
    }

    private void OnBecameInvisible()
    {
        inCameraView = false;
        if (!gameObject.tag.Equals("Boss"))
        {
            firing.DestroyAmmo();
            Destroy(gameObject);
        }
    }

}
