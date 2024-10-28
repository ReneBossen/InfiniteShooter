using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabsWith45Chance;
    [SerializeField] private GameObject[] enemyPrefabsWith35Chance;
    [SerializeField] private GameObject[] enemyPrefabsWith20Chance;
    [SerializeField] private float timeBetweenSpawns;
    private List<GameObject> enemiesToSpawn;
    private int enemiesReadyInSpawner;
    private float timeSinceLastSpawn;

    //Enemy swarm
    private float timeSinceLastSwarm;
    private float timeTillNextSwarm;
    private void Awake()
    {
        enemiesToSpawn = new List<GameObject>();
        timeTillNextSwarm = 15;
        enemiesReadyInSpawner = 10;
    }

    private void Update()
    {
        FillSpawnerWithFixedRandom();
        if (enemiesToSpawn.Count > 0)
        {
            TrySpawnEnemy();
            TrySpawnEnemySwarm();
        }
    }

    private void TrySpawnEnemy()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            Vector3 playerCurrentPosition = Player.Instance.transform.position;
            GameObject enemyToSpawn = enemiesToSpawn[0];

            Instantiate(enemyToSpawn, new Vector3(playerCurrentPosition.x + Random.Range(15, 25), 0, 0), Quaternion.identity);

            enemiesToSpawn.RemoveAt(0);
            timeSinceLastSpawn = 0;
        }
    }
    private void FillSpawnerWithFixedRandom()
    {
        if (enemiesToSpawn.Count < enemiesReadyInSpawner)
        {
            int randomNumberToSpawn = Random.Range(0, 101);
            if (randomNumberToSpawn < 45)
            {
                int randomNumberFromList = Random.Range(0, enemyPrefabsWith45Chance.Length);
                enemiesToSpawn.Add(enemyPrefabsWith45Chance[randomNumberFromList]);
            }
            else if (randomNumberToSpawn > 45 && randomNumberToSpawn <= 80)
            {
                int randomNumberFromList = Random.Range(0, enemyPrefabsWith35Chance.Length);
                enemiesToSpawn.Add(enemyPrefabsWith35Chance[randomNumberFromList]);
            }
            else if (randomNumberToSpawn > 80)
            {
                int randomNumberFromList = Random.Range(0, enemyPrefabsWith20Chance.Length);
                enemiesToSpawn.Add(enemyPrefabsWith20Chance[randomNumberFromList]);
            }
        }
    }

    private void TrySpawnEnemySwarm()
    {
        timeSinceLastSwarm += Time.deltaTime;
        if (timeSinceLastSwarm >= timeTillNextSwarm)
        {
            Vector3 playerCurrentPosition = Player.Instance.transform.position;
            for (int i = 0; i < enemiesToSpawn.Count; i++)
            {
                GameObject enemyToSpawn = enemiesToSpawn[i];
                Instantiate(enemyToSpawn, new Vector3(playerCurrentPosition.x + Random.Range(15, 25), 0, 0), Quaternion.identity);
            }

            enemiesToSpawn.Clear();
            timeSinceLastSwarm = 0;
            timeTillNextSwarm += 10;
            enemiesReadyInSpawner += 2;
        }
    }

    private void EnemyHealthIncreaseOverTime()
    {
        float timeIngame = 0f;
        timeIngame += Time.deltaTime;

        if (timeIngame > 15f)
        {
            timeIngame = 0f;

        }
    }
}
