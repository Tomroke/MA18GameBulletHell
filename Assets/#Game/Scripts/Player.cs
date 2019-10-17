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
    private int bulletsPerShot;

    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float coolDownPerSec = 0.25f;

    [SerializeField]
    [Range(1, 50)]
    private int ammoAmount = 10;

    [SerializeField]
    [Range(1, 50)]
    private int bulletDamage = 1;

    [SerializeField]
    [Range(1, 50)]
    private float bulletSpeed = 3.0f;

    [Range(-360, 360)]
    [SerializeField]
    private float startAngle;

    [Range(-360, 360)]
    [SerializeField]
    private float endAngle; 

    [SerializeField]
    private Sprite bulletSprite;
    [SerializeField]
    private float spriteScale;

    private Vector2 movement;
    private GameObject fireButton;
    private Rigidbody2D rigidbodyComponent;
    private FiringScript fireing;
    private HealthScript health;

    //private static Player _instance;


    //public static Player Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = Instantiate(Resources.Load<GameObject>("Prefab/Player")).GetComponent<Player>();
    //        }

    //        return _instance;
    //    }
    //}


    //void Awake()
    //{
    //    if (_instance != null && _instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        _instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}


    private void Start()
    {

        fireing = GetComponent<FiringScript>();
    }

    public void SetFiringRules(float rateOfFire,    
                               float coolDownPerSec,
                               int bulletsPerShot,
                               int ammoAmount,
                               float startAngle,
                               float endAngle,
                               Sprite bulletSprite,
                               float bulletSpeed,
                               int bulletDamage,
                               float spriteScale)
    {
        fireing.SetFireRules(rateOfFire, coolDownPerSec, bulletsPerShot, ammoAmount, startAngle, endAngle, bulletSprite, bulletSpeed, bulletDamage, spriteScale);
    }


    void Update()
    {
        float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        float inputY = CrossPlatformInputManager.GetAxis("Vertical");
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        foreach (Touch touch in Input.touches)
        {
            //Debug.Log("In foreach");
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == fireButton)
                    {
                        //Debug.Log("Touch Began");
                    }
                    break;

                case TouchPhase.Stationary:
                    if (hit.collider != null && hit.collider.gameObject == fireButton)
                    {
                        //Debug.Log("Touch Stationary");
                        if (fireing != null)
                        {
                            //Debug.Log("In If sats");
                            fireing.Attack();
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    if (hit.collider != null && hit.collider.gameObject == fireButton)
                    {
                        //Debug.Log("Touch Ended");
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
        if (powerUp != null && powerUp.getPowerupType().Equals("new"))
        {
            rateOfFire = powerUp.getRateofFire();
            coolDownPerSec = powerUp.getCoolDownPerSec();
            bulletsPerShot = powerUp.getBulletAmount();
            ammoAmount = powerUp.getAmmoAmount();
            startAngle = powerUp.getStartAngle();
            endAngle = powerUp.getEndAngle();
            bulletSprite = powerUp.getSprite();
            bulletSpeed = powerUp.getSpeed();
            bulletDamage = powerUp.getDamage();

            SetFiringRules(rateOfFire, coolDownPerSec, bulletsPerShot, ammoAmount, startAngle, endAngle, bulletSprite, bulletSpeed, bulletDamage, spriteScale);

            Destroy(powerUp.gameObject);
        }

        else if (powerUp != null && powerUp.getPowerupType().Equals("upgrade"))
        {
            rateOfFire += powerUp.getRateofFire();
            coolDownPerSec += powerUp.getCoolDownPerSec();
            bulletsPerShot += powerUp.getBulletAmount();
            bulletSprite = powerUp.getSprite();
            bulletSpeed += powerUp.getSpeed();
            bulletDamage += powerUp.getDamage();

            SetFiringRules(rateOfFire, coolDownPerSec, bulletsPerShot, ammoAmount, startAngle, endAngle, bulletSprite, bulletSpeed, bulletDamage, spriteScale);

            Destroy(powerUp.gameObject);
        }
    }

    public void InitializePlayerObjects()
    {

        fireButton = GameObject.Find("FireButton");

        health = GetComponent<HealthScript>();
        if (health != null)
        {
            health.InstansiatePlayerHealthBar();
        }

        fireing = GetComponent<FiringScript>();
        if (fireing != null)
        {
            SetFiringRules(rateOfFire, coolDownPerSec, bulletsPerShot, ammoAmount, startAngle, endAngle, bulletSprite, bulletSpeed, bulletDamage, spriteScale);
            fireing.InitiatePlayerAmmo();
        }
    }

}
