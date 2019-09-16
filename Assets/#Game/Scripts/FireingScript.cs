using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireingScript : MonoBehaviour
{
    [SerializeField]
    private Transform shotPrefab;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float shootingRate = 0.25F;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float shotCooldown;

    void Start()
    {
        shotCooldown = 0f;
    }

    void Update()
    {
        if (shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
        } 
    }

    public void Attack (bool enemy)
    {
        if (CanAttack)
        {
            shotCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.IsEnemyShot = enemy;
            }

            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if(move != null)
            {
                move.Direction = this.transform.up;
            }
        }
    }

    private bool CanAttack
    {
        get
        {
            return shotCooldown <= 0f;
        }
    }

}
