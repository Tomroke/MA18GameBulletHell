using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    [Header("Private Variables")]
    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField]
    private float spawnDelay = 10.0f;
    
    private GameObject enemyPrefabOne;
    private GameObject enemyPrefabTwo;
    private GameObject enemyPrefabBoss;

    private bool bossNotActive = true;
    private int previousNumb;

    private void Start()
    {
        enemyPrefabOne = Resources.Load<GameObject>("Prefab/Enemy01");
        enemyPrefabTwo = Resources.Load<GameObject>("Prefab/Enemy02");
        enemyPrefabBoss = Resources.Load<GameObject>("Prefab/Boss01");

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (bossNotActive)
        {
            yield return new WaitForSeconds(spawnDelay);
            instantiateEnemy();
        }
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

    public int EnemyType()
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

    public int RandomIndex()
    {
        return UnityEngine.Random.Range(0, spawnPoints.Count-1);
    }

}
