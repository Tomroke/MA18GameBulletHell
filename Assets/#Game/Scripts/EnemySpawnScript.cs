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

    private void Start()
    {
        Debug.Log("Spawning has Started");
        enemyPrefab.transform.position = spawnPoints[RandomIndex()].transform.position;
        StartCoroutine("SpawnEnemies");
    }

    IEnumerator SpawnEnemies()
    {
        Debug.Log("In Coroutine");
        while (bossNotActive)
        {
            Debug.Log("in While loop");
            yield return new WaitForSeconds(spawnDelay);
            instantiateEnemy();
            Debug.Log("Spawn Active");
        }
    }

    private void instantiateEnemy()
    {
        enemyPrefab.transform.position = spawnPoints[RandomIndex()].transform.position;
        Instantiate(enemyPrefab);
    }

    public int RandomIndex()
    {
        return UnityEngine.Random.Range(0, spawnPoints.Count-1);
    }

}
