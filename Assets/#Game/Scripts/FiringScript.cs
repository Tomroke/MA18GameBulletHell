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

    private float bulletSpeed;

    private int bulletDamage;

    private Sprite bulletSprite;

    private float spriteScale;

    private GameObject bulletObject;

    private GameObject ammoGameObject;

    private bool enemyShots = false;

    private Vector3 shootersCurrentPosition;

    private List<GameObject> ammoBelt = new List<GameObject>();

    private void Awake()
    {
        bulletObject = Resources.Load<GameObject>("Prefab/ProjectileOne");
        ammoGameObject = GameObject.Find("AmmoBag");
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
            bulletObject.GetComponent<SpriteRenderer>().sprite = bulletSprite;
            bulletObject.GetComponent<Transform>().localScale = new Vector3(spriteScale, spriteScale, 0);
            GameObject _Bullet = Instantiate(bulletObject, gameObject.transform.position, Quaternion.identity);
            _Bullet.transform.SetParent(ammoGameObject.transform, false);
            if (_Bullet != null)
            {
                _Bullet.GetComponent<Rigidbody2D>().GetComponent<ShotScript>().IsEnemyShot = enemyShots;
                _Bullet.GetComponent<ShotScript>().SetDamage = bulletDamage;
                _Bullet.GetComponent<ShotScript>().SetSpeed = bulletSpeed;
                _Bullet.SetActive(false);
                ammoBelt.Add(_Bullet);
            }
        }
    }

    public void InitiatePlayerAmmo()
    {
        for (int i = 0; i < ammoAmount; i++)
        {
            bulletObject.GetComponent<SpriteRenderer>().sprite = bulletSprite;
            bulletObject.GetComponent<Transform>().localScale = new Vector3(spriteScale, spriteScale, 0);
            GameObject _Bullet = Instantiate(bulletObject, gameObject.transform.position, Quaternion.identity);
            //_Bullet.transform.SetParent(ammoGameObject.transform, false);
            if (_Bullet != null)
            {
                _Bullet.GetComponent<Rigidbody2D>().GetComponent<ShotScript>().IsEnemyShot = enemyShots;
                _Bullet.GetComponent<ShotScript>().SetDamage = bulletDamage;
                _Bullet.GetComponent<ShotScript>().SetSpeed = bulletSpeed;
                _Bullet.SetActive(false);
                ammoBelt.Add(_Bullet);
            }
        }
    }


    public void Attack()
    {
        if (CanAttack)
        {
            ChangeSprite();
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

    public void ChangeSprite()
    {
        foreach (GameObject gameObject in ammoBelt)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprite;
            }
        }
    }

    public GameObject GetAmmo()
    {
        foreach (GameObject _Bullet in ammoBelt)
        {
            if (!_Bullet.gameObject.activeInHierarchy)
            {
                return _Bullet;
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
                             Sprite bulletSprite,
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

        this.bulletSprite = bulletSprite;

        this.spriteScale = spriteScale;

        this.bulletDamage = bulletDamage;

        this.bulletSpeed = bulletSpeed;

    }


    //Sets players firerules
    public void SetFireRules(float rateOfFire,
                             float coolDownPerSec,
                             int bulletsPerShot,
                             int ammoAmount,
                             float startAngle,
                             float endAngle,
                             Sprite bulletSprite,
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

        this.bulletDamage = bulletDamage;

        this.bulletSpeed = bulletSpeed;

        if (this.bulletSprite != bulletSprite)
        {
        this.bulletSprite = bulletSprite;

        ChangeSprite();
        }

        this.spriteScale = spriteScale;


    }

}
