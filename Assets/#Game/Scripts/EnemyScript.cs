using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private FiringScript firing;

    void Awake()
    {
        firing = GetComponent<FiringScript>();
        //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.Find("QuadWall (2)").GetComponent<Collider>());
        //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.Find("QuadWall (3)").GetComponent<Collider>());
    }

    void Start()
    {
        firing.setAsynchronousShooting(true);
    }

    void Update()
    {
        if (firing != null && firing.CanAttack)
        {
            firing.Attack(true);
        }
    }

}
