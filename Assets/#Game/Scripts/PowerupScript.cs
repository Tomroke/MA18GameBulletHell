using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [Header("Power up: new or upgrade")]
    [SerializeField]
    private string PowerupType;

    [Header("Variables")]
    [Range(0f, 5.0f)]
    [SerializeField]
    private float rateOfFire;

    [Range(0, 100)]
    [SerializeField]
    private int bulletsPerShot;

    [Range(0f, 5.0f)]
    [SerializeField]
    private float coolDownPerSec;

    [Range(0, 50)]
    [SerializeField]
    private int ammoAmount;

    [Range(-360, 360)]
    [SerializeField]
    private float startAngle;

    [Range(-360, 360)]
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

    public string getPowerupType()
    {
        return PowerupType;
    }

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
