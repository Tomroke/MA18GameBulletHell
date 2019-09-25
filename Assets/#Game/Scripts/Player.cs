using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed = new Vector2(50, 50);

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float shootingRate = 0.5f;

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float coolDown = 0.25f;

    [Range(1, 50)]
    [SerializeField]
    private int ammoAmount = 10;


    [SerializeField]
    private float fireDirectionY = 9.0f;

    private float collisionDamageAmount = 1.0f;
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    FiringScript fireing;

    private static Player _instance;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("Prefab/Player")).GetComponent<Player>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        fireing = GetComponent<FiringScript>();
        fireing.SetFireRules(shootingRate, coolDown, ammoAmount);
    }

    void Update()
    {
        float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        float inputY = CrossPlatformInputManager.GetAxis("Vertical");
        movement = new Vector2(speed.x * inputX, speed.y * inputY);


        //if (CrossPlatformInputManager.GetButtonDown("FireingButton"))
        //{
        //    fireing = GetComponent<FiringScript>();

        //    if (fireing != null)
        //    {
        //        fireing.Attack(fireDirectionY);
        //    }
        //}

        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            fireing = GetComponent<FiringScript>();
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == GameObject.Find("FireButton"))
                    {
                        Debug.Log("Touch Began");
                    }
                    break;

                case TouchPhase.Stationary:
                    if (hit.collider != null && hit.collider.gameObject == GameObject.Find("FireButton"))
                    {
                        if (fireing != null)
                        {
                            fireing.Attack(fireDirectionY);
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    if (hit.collider != null && hit.collider.gameObject == GameObject.Find("FireButton"))
                    {
                        Debug.Log("Touch Ended");
                    }
                    break;
            }

        }

    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null)
                enemyHealth.Damage(enemyHealth.Health());

            damagePlayer = true;
        }

        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null)
                playerHealth.Damage(collisionDamageAmount);
        }

    }



}
