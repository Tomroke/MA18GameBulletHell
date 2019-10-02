using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript: MonoBehaviour
{

    private float startAngle;

    private float endAngle;

    private const float radius = 1F;

    private float rateOfFire;

    private float coolDownPerSec;

    private int ammoAmount;

    private int bulletsPerShot;

    private Sprite bulletSpite;

    private float spriteScale;

    private GameObject bulletObject;

    private bool enemyShots = false;

    private Vector3 shootersCurrentPosition;

    List<GameObject> ammoBelt = new List<GameObject>();

    private void Awake()
    {
        bulletObject = Resources.Load<GameObject>("Prefab/ProjectileOne");
    }

    void Start()
    {
        InitiateAmmo();
        coolDownPerSec = 0f;
    }


    void Update()
    {
        if (coolDownPerSec > 0)
        {
            coolDownPerSec -= (Time.deltaTime);
        } 

    }


    private void InitiateAmmo()
    {
        for (int i = 0; i < ammoAmount; i++)
        {
            bulletObject.GetComponent<SpriteRenderer>().sprite = bulletSpite;
            bulletObject.GetComponent<Transform>().localScale = new Vector3(spriteScale, spriteScale, 0);
            GameObject bulletsPerShot = Instantiate(bulletObject, gameObject.transform.position, Quaternion.identity);
            if (bulletsPerShot != null)
            {
                bulletsPerShot.GetComponent<Rigidbody2D>().GetComponent<ShotScript>().IsEnemyShot = enemyShots;
                bulletsPerShot.SetActive(false);
                ammoBelt.Add(bulletsPerShot);
            }
        }
    }


    public void Attack()
    {
        if (CanAttack)
        {
            coolDownPerSec = rateOfFire;
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        float angleStep = (endAngle - startAngle) / (bulletsPerShot-1);
        float angle = startAngle;

            for (int i = 0; i < bulletsPerShot; i++)
            {
                float projectileDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f) * radius;
                float projectileDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f) * radius;

                Vector3 projectileVector = new Vector3(projectileDirX, projectileDirY , 0.0f);
                Vector2 projectileMoveDirection = (projectileVector - transform.position).normalized;

                GameObject _Bullet = GetAmmo();
                _Bullet.transform.position = transform.position;
                _Bullet.SetActive(true);
                _Bullet.GetComponent<ShotScript>().StartAnimation(projectileMoveDirection);

                angle += angleStep;
            }
    }

    public GameObject GetAmmo()
    {
        foreach (GameObject bulletsPerShot in ammoBelt)
        {
            if (!bulletsPerShot.gameObject.activeInHierarchy)
            {
                return bulletsPerShot;
            }
            
        }
        return null;
    }


    public bool CanAttack
    {
        get { return coolDownPerSec <= 0f; }
    }


    public void DestroyAmmo()
    {
        for (int i = ammoBelt.Count-1; i >=0; i--)
        {
            Destroy(ammoBelt[i].gameObject);
            ammoBelt.RemoveAt(i);
        }
    }

    //Sets enemies firerules
    public void SetFireRules(float rateOfFire,
                             float coolDownPerSec,
                             int bulletsPerShot,
                             int ammoAmount,
                             bool enemyShots,
                             float startAngle,
                             float endAngle,
                             Sprite bulletSpite,
                             float bulletSpeed,
                             int bulletDamage,
                             float spriteScale)
    {

        this.rateOfFire = rateOfFire;

        this.coolDownPerSec = coolDownPerSec;

        this.bulletsPerShot = bulletsPerShot;

        this.ammoAmount = ammoAmount;

        this.enemyShots = enemyShots;

        this.startAngle = startAngle;

        this.endAngle = endAngle;

        this.bulletSpite = bulletSpite;

        this.spriteScale = spriteScale;

        if (bulletObject != null)
        {

        bulletObject.GetComponent<ShotScript>().SetSpeed = bulletSpeed;

        bulletObject.GetComponent<ShotScript>().SetDamage = bulletDamage;

        }

    }


    //Sets players firerules
    public void SetFireRules(float rateOfFire,
                             float coolDownPerSec,
                             int bulletsPerShot,
                             int ammoAmount,
                             float startAngle,
                             float endAngle,
                             Sprite bulletSpite,
                             float bulletSpeed,
                             int bulletDamage,
                             float spriteScale)
    {
        this.rateOfFire = rateOfFire;

        this.coolDownPerSec = coolDownPerSec;

        this.bulletsPerShot = bulletsPerShot;

        this.ammoAmount = ammoAmount;

        this.startAngle = startAngle;

        this.endAngle = endAngle;

        this.bulletSpite = bulletSpite;

        this.spriteScale = spriteScale;

        if (bulletObject != null)
        {

        bulletObject.GetComponent<ShotScript>().SetSpeed = bulletSpeed;

        bulletObject.GetComponent<ShotScript>().SetDamage = bulletDamage;

        }
    }

}
