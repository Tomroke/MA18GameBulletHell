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

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    private GameObject enemyPrefabBoss;

    [SerializeField]
    private List<GameObject> powerups;
    private int powerUpNum = 1;

    private bool bossNotActive = true;
    private int previousNumb;


    private void Start()
    {
        enemyPrefabBoss = Resources.Load<GameObject>("Prefab/Boss01");

        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        StartCoroutine(bossCountdown());
        while (bossNotActive)
        {
            StartCoroutine(instantiatePowerup());
            yield return new WaitForSeconds(enemySpawnDelay);
            instantiateGameObject(GenerateRandomEnemy(), RandomIndex());
        }
    }

    IEnumerator bossCountdown()
    {
        yield return new WaitForSeconds(bossSpawnTimer);
        bossNotActive = false;
        instantiateGameObject(enemyPrefabBoss, 6);
    }

    IEnumerator instantiatePowerup()
    {
        yield return new WaitForSeconds(powerUpSpawnDelay);
        instantiateGameObject(GenerateRandomPowerup(), RandomIndex());
    }

    private GameObject GenerateRandomEnemy()
    {
        int tmp = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        return enemyPrefabs[tmp];
    }

    private GameObject GenerateRandomPowerup()
    {
        int tmp = UnityEngine.Random.Range(0, powerups.Count);
        return powerups[tmp];
        
    }

    private void instantiateGameObject(GameObject gameObject, int listIndex)
    {
        gameObject.GetComponent<MoveScript>().SetParentPosition(spawnPoints[listIndex].transform.position);
        gameObject.transform.position = spawnPoints[listIndex].transform.position;
        gameObject.GetComponent<MoveScript>().InitiateMovement();
        gameObject.SetActive(true);
        Instantiate(gameObject);
        gameObject.SetActive(true);
            
    }

    private int RandomIndex()
    {
        return UnityEngine.Random.Range(0, spawnPoints.Count-1);
    }
}
