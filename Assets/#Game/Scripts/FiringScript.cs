using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript: MonoBehaviour
{
    [SerializeField]
    private GameObject shotPrefab;

    [SerializeField]
    private float startAngle = 0f, endAngle = 90f;

    private const float radius = 1F;

    private float rateOfFire;

    private float cooldown;

    private int ammoAmount;

    private int bulletAmount;

    private bool enemyShots = false;

    private bool isAllowedToAttack = true;

    private Vector3 shootersCurrentPosition;

    List<GameObject> ammoBelt = new List<GameObject>();

    private float fireDirectionY = -9.0f;

    void Start()
    {
        InitiateAmmo();
        cooldown = 0f;
    }


    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= (Time.deltaTime);
        } 

    }


    private void InitiateAmmo()
    {
        for (int i = 0; i < ammoAmount; i++)
        {
            GameObject bullet = Instantiate(shotPrefab, gameObject.transform.position, Quaternion.identity);
            if (bullet != null)
            {
                bullet.GetComponent<Rigidbody2D>().GetComponent<ShotScript>().IsEnemyShot = enemyShots;
                bullet.SetActive(false);
                ammoBelt.Add(bullet);
            }
        }
    }


    //Enemies Attack
    public void Attack()
    {
        if (CanAttack)
        {
            cooldown = rateOfFire;
            SpawnProjectile();
        }
    }

    //Player Attack
    public void Attack(float newY)
    {
        if (CanAttack)
        {
            cooldown = rateOfFire;
            GameObject bullet = GetAmmo();

            if (bullet != null)
            {
                bullet.transform.position = gameObject.transform.position;
            }

        }
    }

    private void SpawnProjectile()
    {
        float angleStep = (endAngle - startAngle) / (bulletAmount-1);
        float angle = startAngle;

            for (int i = 0; i < bulletAmount; i++)
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
        foreach (GameObject bullet in ammoBelt)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                return bullet;
            }
            
        }
        return null;
    }


    public bool CanAttack
    {
        get { return cooldown <= 0f; }
    }


    public void DestroyAmmo()
    {
        for (int i = ammoBelt.Count-1; i >=0; i--)
        {
            Destroy(ammoBelt[i].gameObject);
            ammoBelt.RemoveAt(i);
        }
    }


    public void SetFireRules(float rateOfFire, float cooldown, int bulletAmount, int ammo, bool enemy)
    {
        this.rateOfFire = rateOfFire;

        this.cooldown = cooldown;

        this.bulletAmount = bulletAmount;

        ammoAmount = ammo;

        enemyShots = enemy;
    }


    public void SetFireRules(float rateOfFire, float cooldown, int bulletAmount, int ammo)
    {
        this.rateOfFire = rateOfFire;

        this.cooldown = cooldown;

        this.bulletAmount = bulletAmount;

        ammoAmount = ammo;
    }

}
