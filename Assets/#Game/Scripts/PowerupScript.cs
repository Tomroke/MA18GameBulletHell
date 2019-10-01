using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float rateOfFire;

    [Range(1, 100)]
    [SerializeField]
    private int bulletAmount;

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


    public float getRateofFire()
    {
        return rateOfFire;
    }

    public int getBulletAmount()
    {
        return bulletAmount;
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
}
