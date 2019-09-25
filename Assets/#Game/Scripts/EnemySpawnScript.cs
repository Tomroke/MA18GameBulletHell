using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField]
    private float spawnDelay = 10.0f;
    
    [SerializeField]
    private GameObject enemyPrefab;

    private bool bossNotActive = true;
    private int previousNumb;

    private void Start()
    {
        enemyPrefab.transform.position = spawnPoints[RandomIndex()].transform.position;
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
            if (previousNumb != newNumb)
            {
                previousNumb = newNumb;
                enemyPrefab.GetComponent<MoveScript>().SetParentPosition(spawnPoints[newNumb].transform.position);
                enemyPrefab.transform.position = spawnPoints[newNumb].transform.position;
                enemyPrefab.GetComponent<MoveScript>().InitiateMovement();
                Instantiate(enemyPrefab);
                enemyHasSpawned = true;
            }
        }
    }

    public int RandomIndex()
    {
        return UnityEngine.Random.Range(0, spawnPoints.Count-1);
    }

}
