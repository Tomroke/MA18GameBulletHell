using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript: MonoBehaviour
{
    [SerializeField]
    private GameObject shotPrefab;

    private float shootingRate;

    private float shotCooldown;

    private int ammoAmount;

    private bool enemyShots = false;

    private bool isAllowedToAttack = true;

    List<Rigidbody2D> ammoBelt = new List<Rigidbody2D>();

    void Start()
    {
        InitiateAmmo();
        shotCooldown = 0f;
    }


    void Update()
    {
        if (shotCooldown > 0)
        {
            shotCooldown -= (Time.deltaTime);
        } 
    }

    private void InitiateAmmo()
    {
        for (int i = 0; i < ammoAmount; i++)
        {
            GameObject bullet = Instantiate(shotPrefab);
            bullet.SetActive(false);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            ShotScript shot = rb.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.IsEnemyShot = enemyShots;
            }

            ammoBelt.Add(rb);
        }
    }

    public void Attack ()
    {
        if (CanAttack && isAllowedToAttack)
        {
            shotCooldown = shootingRate;
            Rigidbody2D rb = GetAmmo();

            if (rb != null)
            {
                rb.gameObject.GetComponent<ShotScript>().StartAnimation();
                rb.transform.position = transform.position;
            }
        }
    }

    Rigidbody2D GetAmmo()
    {
        foreach (Rigidbody2D bullet in ammoBelt)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }
        return null;
    }

    public bool CanAttack
    {
        get { return shotCooldown <= 0f; }
    }

    public void DestroyAmmo()
    {
        for (int i = ammoBelt.Count-1; i >=0; i--)
        {
            Destroy(ammoBelt[i].gameObject);
            ammoBelt.RemoveAt(i);
        }
    }

    public void SetIsAllowedToAttack(bool check)
    {
        isAllowedToAttack = check;
    }

    public void SetFireRules(float rate, float cooldown, int ammo, bool enemy)
    {
        shootingRate = rate;

        shotCooldown = cooldown;

        ammoAmount = ammo;

        enemyShots = enemy;
    }

    public void SetFireRules(float rate, float cooldown, int ammo)
    {
        shootingRate = rate;

        shotCooldown = cooldown;

        ammoAmount = ammo;
    }

}
