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
        //Debug.Log("Enemy Spawned");
        int ranNumb = RandomIndex();
        enemyPrefab.transform.position = spawnPoints[ranNumb].transform.position;
        enemyPrefab.GetComponent<MoveScript>().SetParentPosition(
                                                                 spawnPoints[ranNumb].transform.position.x,
                                                                 spawnPoints[ranNumb].transform.position.y,
                                                                 spawnPoints[ranNumb].transform.position.z);
        enemyPrefab.GetComponent<MoveScript>().InitiateMovement();
        Instantiate(enemyPrefab);
    }

    public int RandomIndex()
    {
        return UnityEngine.Random.Range(0, spawnPoints.Count-1);
    }

}
