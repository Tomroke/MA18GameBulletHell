﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Universal Variables")]
    [SerializeField]
    private int health = 1;

    [Header("Enemy Variables")]
    [SerializeField]
    private bool isEnemy = true;

    [Header("Flash Colour Variables")]
    [SerializeField]
    Color flashColour;

    [SerializeField]
    Color normalColour;

    [SerializeField]
    int numFlashes = 3; 

    [SerializeField]
    private float iFrameSeconds = 2.0f;

    [SerializeField]
    private float flashSeconds = 0.08f;

    private bool iFrame = false;

    private int collisionDamage = 1;

    private SpriteRenderer playerSprite;

    private SpriteRenderer enemySprite;

    private GameObject playerHealthIcon;
    private List<GameObject> playerHealthList = new List<GameObject>();

    private void Start()
    {
        if (gameObject.tag.Equals("Player"))
        {
            playerSprite = gameObject.GetComponent<SpriteRenderer>();
        }

        if (!gameObject.tag.Equals("Player"))
        {
            enemySprite = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    public void Damage(int damageAmount)
    {
        if (!iFrame)
        {
            health -= damageAmount;

            if (gameObject.tag.Equals("Player") && health >= 0)
            {
                GameObject temp = playerHealthList[health];
                temp.SetActive(false);
                StartCoroutine(IFrames(playerSprite));
            }

            if (!gameObject.tag.Equals("Player") && health >= 0)
            {
                Debug.Log("in IFrame");
                StartCoroutine(ColourFlash(enemySprite));
            }
        }

        if (health <= 0)
        {
            if (!gameObject.tag.Equals("Player"))
            {
                GameManager.Instance.IncreaseScore();
            }

            if (gameObject.tag.Equals("Player"))
            {
                GameManager.Instance.LevelOver();
            }

            //Change this so the bullets remains after the shooter is destroyed.
            gameObject.GetComponent<FiringScript>().DestroyAmmo();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();
        if(shot != null && !iFrame)
        {
            if (shot.IsEnemyShot != isEnemy)
            {
            Damage(shot.DamageAmount());
            shot.gameObject.SetActive(false);
            }
        }

        HealthScript enemy = collision.gameObject.GetComponent<HealthScript>();
        if (enemy != null && !iFrame)
        {
            if (!enemy.IsEnemy())
            {
                Damage(collisionDamage);
                enemy.Damage(collisionDamage);
            }
        }
    }

    public void InstansiatePlayerHealthBar()
    {
        if (gameObject.tag.Equals("Player"))
        {
            for (int index = 0; index < health; index++)
            {
                playerHealthIcon = Resources.Load<GameObject>("Prefab/PlayerLifePoint");
                float tempPos = -8;
                playerHealthIcon.GetComponent<Transform>().LeanSetPosX(tempPos+index);
                playerHealthIcon.SetActive(true);
                playerHealthList.Add(Instantiate(playerHealthIcon));
            }
        }
    }

    IEnumerator IFrames(SpriteRenderer playersSprite)
    {
        StartCoroutine(ColourFlash(playersSprite));
        iFrame = true;
        yield return new WaitForSeconds(iFrameSeconds);
        iFrame = false;
    }

    IEnumerator ColourFlash(SpriteRenderer spriteRenderer)
    {
        int temp = 0;
        if (spriteRenderer != null)
        {
            while (temp < numFlashes)
            {
                spriteRenderer.color = flashColour;
                yield return new WaitForSeconds(flashSeconds);
                spriteRenderer.color = normalColour;
                yield return new WaitForSeconds(flashSeconds);
                temp++;
            }
        }
    }

    public void ResetPlayerHealth()
    {
            foreach (GameObject tmp in playerHealthList)
            {
                tmp.SetActive(true);
            }
    }

    private void OnBecameVisible()
    {
        iFrame = false;
    }

    private void OnBecameInvisible()
    {
        iFrame = true;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void SetPlayerHealth(int tmp)
    {
        health = tmp;
    }

}
