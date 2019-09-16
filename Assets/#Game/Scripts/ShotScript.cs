using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{

    [SerializeField]
    private float damage = 1.0f;

    [SerializeField]
    private bool isEnemyShot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 15);
    }

    public bool IsEnemyShot
    {
        get { return isEnemyShot; }
        set { isEnemyShot = value; }
    }
}
