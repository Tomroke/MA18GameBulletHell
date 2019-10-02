﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float rateOfFire;

    [Range(1, 100)]
    [SerializeField]
    private int bulletsPerShot;

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float coolDownPerSec;

    [Range(1, 50)]
    [SerializeField]
    private int ammoAmount;

    [Range(0, 360)]
    [SerializeField]
    private float startAngle;

    [Range(0, 360)]
    [SerializeField]
    private float endAngle;

    [Range(0, 10)]
    [SerializeField]
    private float speed;

    [Range(0, 5)]
    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite bulletSprite;
    

    public float getRateofFire()
    {
        return rateOfFire;
    }

    public int getBulletAmount()
    {
        return bulletsPerShot;
    }

    public float getCoolDownPerSec()
    {
        return coolDownPerSec;
    }

    public int getAmmoAmount()
    {
        return ammoAmount;
    }

    public float getStartAngle()
    {
        return startAngle;
    }

    public float getEndAngle()
    {
        return endAngle;
    }

    public float getSpeed()
    {
        return speed;
    }

    public int getDamage()
    {
        return damage;
    }

    public Sprite getSprite()
    {
        return bulletSprite;
    }
}
