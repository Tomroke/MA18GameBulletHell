using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private FiringScript firing;

    private bool inCameraView = false;

    [Header("Bullet Variables")]
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float rateOfFire = 0.5f;

    [Range(1, 100)]
    [SerializeField]
    private int bulletAmount;

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float coolDownPerSec = 0.25f;

    [Range(1, 50)]
    [SerializeField]
    private int ammoAmount = 10;

    [Range(0, 360)]
    [SerializeField]
    private float startAngle;

    [Range(0, 360)]
    [SerializeField]
    private float endAngle;

    void Awake()
    {
        firing = GetComponent<FiringScript>();
        firing.SetFireRules(rateOfFire, coolDownPerSec, bulletAmount, ammoAmount, true, startAngle, endAngle);
    }

    void Update()
    {
        if (firing != null && firing.CanAttack && inCameraView)
        {
            firing.Attack();
        }
    }

    private void OnBecameVisible()
    {
        inCameraView = true;
    }

    private void OnBecameInvisible()
    {

        inCameraView = false;
        firing.DestroyAmmo();
        Destroy(gameObject);
    }

}
