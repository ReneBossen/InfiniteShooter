using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_OLD : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabsWith45Chance;
    [SerializeField] private GameObject[] enemyPrefabsWith35Chance;
    [SerializeField] private GameObject[] enemyPrefabsWith20Chance;
    [SerializeField] private float timeBetweenSpawns;
    private List<GameObject> enemiesToSpawn;
    private float timeSinceLastSpawn;

    private void Awake()
    {
        enemiesToSpawn = new List<GameObject>();
    }

    private void Update()
    {
        FillSpawnerWithFixedRandom();
        if (enemiesToSpawn.Count > 0)
        {
            TrySpawnEnemy();
        }
    }

    private void TrySpawnEnemy()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            Vector3 playerCurrentPosition = Player.Instance.transform.position;
            int spawnLeftSide = Random.Range(0, 2);
            GameObject enemyToSpawn = enemiesToSpawn[0];
            if (spawnLeftSide == 1)
            {
                GameObject enemy = Instantiate(enemyToSpawn, new Vector3(playerCurrentPosition.x - Random.Range(15, 25), 0, 0), Quaternion.identity);
                if (enemy.transform.position.x < -50)
                {
                    enemy.transform.position = new Vector3(-35, 0, 0);
                }
            }
            else
            {
                GameObject enemy = Instantiate(enemyToSpawn, new Vector3(playerCurrentPosition.x + Random.Range(15, 25), 0, 0), Quaternion.identity);
                if (enemy.transform.position.x > 50)
                {
                    enemy.transform.position = new Vector3(35, 0, 0);
                }
            }
            enemiesToSpawn.RemoveAt(0);
            timeSinceLastSpawn = 0;
        }
    }
    private void FillSpawnerWithFixedRandom()
    {
        if (enemiesToSpawn.Count < 10)
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
}
