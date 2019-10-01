using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField]
    private Vector2 speed = new Vector2(50, 50);

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
        SetFiringRules(rateOfFire, coolDownPerSec, bulletAmount, ammoAmount, startAngle, endAngle);
    }


    public void SetFiringRules(float rof, float cooldown, int bullet, int ammo, float startAngleIn, float endAngleIn)
    {
        fireing.SetFireRules(rof, cooldown, bullet, ammo, startAngleIn, endAngleIn);
    }


    void Update()
    {
        float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        float inputY = CrossPlatformInputManager.GetAxis("Vertical");
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

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
                            fireing.Attack();
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


    void OnTriggerEnter2D(Collider2D collision)
    {
        PowerupScript powerUp = collision.gameObject.GetComponent<PowerupScript>();
        if (powerUp != null)
        {
            rateOfFire = powerUp.getRateofFire();
            coolDownPerSec = powerUp.getCoolDownPerSec();
            bulletAmount = powerUp.getBulletAmount();
            ammoAmount = powerUp.getAmmoAmount();
            startAngle = powerUp.getStartAngle();
            endAngle = powerUp.getEndAngle();

            SetFiringRules(rateOfFire, coolDownPerSec, bulletAmount, ammoAmount, startAngle, endAngle);

            Destroy(powerUp.gameObject);
        }
    }



}
