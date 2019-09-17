using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript: MonoBehaviour
{
    [SerializeField]
    private Transform shotPrefab;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float shootingRate = 0.25F;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float shotCooldown;

    [SerializeField]
    private float minLag = 0.0001f;

    [SerializeField]
    private float maxLag = 0.05f;

    private bool isShootingRateAsynchronous = false;

    void Start()
    {
        shotCooldown = 0f;
    }

    void Update()
    {
        if (shotCooldown > 0)
        {
            shotCooldown -= (Time.deltaTime + asynchronousShooting());
        } 
    }

    public float asynchronousShooting()
    {
        float lag = 0.0f;

        if (isShootingRateAsynchronous)
        {
            lag = Random.Range(minLag, maxLag);
            return lag;
        }
        else
        {
            return lag;
        }
    }

    public void Attack (bool isEnemy)
    {
        if (CanAttack)
        {

            shotCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.IsEnemyShot = isEnemy;
            }

            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if(move != null)
            {
                move.Direction = this.transform.up;
            }
        }
    }

    public bool CanAttack
    {
        get { return shotCooldown <= 0f; }
    }

    public void setAsynchronousShooting(bool temp)
    {
        isShootingRateAsynchronous = temp;
    }

}
