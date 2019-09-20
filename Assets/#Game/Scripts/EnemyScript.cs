using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private FiringScript firing;

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float shootingRate = 0.5f;

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float coolDown = 0.25f;

    [Range(1, 50)]
    [SerializeField]
    private int ammoAmount = 10;

    void Awake()
    {
        firing = GetComponent<FiringScript>();
        firing.SetFireRules(shootingRate, coolDown, ammoAmount, true);
    }

    void Update()
    {
        if (firing != null && firing.CanAttack)
        {
            firing.Attack();
        }
    }

    private void OnBecameInvisible()
    {
        firing.SetIsAllowedToAttack(false);
        firing.DestroyAmmo();
        Destroy(gameObject);
    }

}
