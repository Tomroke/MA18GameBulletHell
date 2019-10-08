using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnScript : MonoBehaviour
{
    [Header("Private Variables")]
    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField]
    private float enemySpawnDelay = 10.0f;

    [SerializeField]
    private float bossSpawnTimer = 10.0f;

    [SerializeField]
    private float powerUpSpawnDelay = 10.0f;

    private GameObject enemyPrefabOne;
    private GameObject enemyPrefabTwo;
    private GameObject enemyPrefabBoss;

    private GameObject powerup;
    private int powerUpNum = 1;

    private bool bossNotActive = true;
    private int previousNumb;


    private void Start()
    {
        enemyPrefabOne = Resources.Load<GameObject>("Prefab/Enemy01");
        enemyPrefabTwo = Resources.Load<GameObject>("Prefab/Enemy02");
        enemyPrefabBoss = Resources.Load<GameObject>("Prefab/Boss01");

        powerup = Resources.Load<GameObject>("Prefab/Powerup01");

        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        StartCoroutine(bossCountdown());
        while (bossNotActive)
        {
            StartCoroutine(instantiatePowerup());
            yield return new WaitForSeconds(enemySpawnDelay);
            instantiateEnemy();
        }
    }

    IEnumerator bossCountdown()
    {
        yield return new WaitForSeconds(bossSpawnTimer);
        bossNotActive = false;
        instantiateBoss();
    }

    IEnumerator instantiatePowerup()
    {

        yield return new WaitForSeconds(powerUpSpawnDelay);
        instantiatePowerUp();
    }


    private void instantiateEnemy()
    {
        bool enemyHasSpawned = false;
        while (!enemyHasSpawned)
        {
            int newNumb = RandomIndex();
            if (previousNumb != newNumb && EnemyType() == 1)
            {
                previousNumb = newNumb;
                enemyPrefabOne.GetComponent<MoveScript>().SetParentPosition(spawnPoints[newNumb].transform.position);
                enemyPrefabOne.transform.position = spawnPoints[newNumb].transform.position;
                enemyPrefabOne.GetComponent<MoveScript>().InitiateMovement();
                enemyPrefabOne.SetActive(true);
                Instantiate(enemyPrefabOne);
                enemyHasSpawned = true;
            }
            if (previousNumb != newNumb && EnemyType() == 2)
            {
                previousNumb = newNumb;
                enemyPrefabTwo.GetComponent<MoveScript>().SetParentPosition(spawnPoints[newNumb].transform.position);
                enemyPrefabTwo.transform.position = spawnPoints[newNumb].transform.position;
                enemyPrefabTwo.GetComponent<MoveScript>().InitiateMovement();
                enemyPrefabTwo.SetActive(true);
                Instantiate(enemyPrefabTwo);
                enemyHasSpawned = true;
            }
        }
    }


    private void instantiatePowerUp()
    {
        int newNumb = RandomIndex();
        powerup.GetComponent<MoveScript>().SetParentPosition(spawnPoints[newNumb].transform.position);
        powerup.transform.position = spawnPoints[newNumb].transform.position;
        powerup.GetComponent<MoveScript>().InitiateMovement();
        powerup.SetActive(true);
        Instantiate(powerup);
    }

    private void instantiateBoss()
    {
        enemyPrefabBoss.GetComponent<MoveScript>().SetParentPosition(spawnPoints[7].transform.position);
        enemyPrefabBoss.transform.position = spawnPoints[7].transform.position;
        enemyPrefabBoss.GetComponent<MoveScript>().InitiateMovement();
        enemyPrefabBoss.SetActive(true);
        Instantiate(enemyPrefabBoss);
    }

    private int EnemyType()
    {
        int enemyNum = UnityEngine.Random.Range(1, 6);
        if (enemyNum <= 3)
        {
            return 1;
        }

        if (enemyNum >= 4)
        {
            return 2;
        }
        return 0;
    }


    private int RandomIndex()
    {
        return UnityEngine.Random.Range(0, spawnPoints.Count-1);
    }
}
