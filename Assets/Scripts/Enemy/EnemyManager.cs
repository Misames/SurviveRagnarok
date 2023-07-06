using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private float difficultyScalingFactor = 0.75f;

    private Transform[] enemySpawns;
    private Transform enemyObjectif;

    public float timeBetweenSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;


    private void Awake()
    {
    }

    public void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;

        }
        
    }

    public int GetEnnemiesAlive()
    {
        return enemiesAlive;
    }

    public void SpawnEnemies(int i)
    {
        enemiesLeftToSpawn = i;
    }
    
    private void SpawnEnemy()
    {
        int randomEnemyType = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[randomEnemyType];

        int randomSpawn = Random.Range(0, enemySpawns.Length);
        GameObject newEnemy = Instantiate(prefabToSpawn, enemySpawns[randomSpawn].position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().setTarget(enemyObjectif.position);
        newEnemy.GetComponent<Enemy>().SetEnemyManager(GetComponent<EnemyManager>());
    }

    public void SetSpawnPoints(Transform[] spawns)
    {
        enemySpawns = spawns;
    }
    
    public void SetObjectif(Transform objectif)
    {
        enemyObjectif = objectif;
    }
}
