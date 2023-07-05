using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int baseEnemies= 10;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy= new UnityEvent();

    private Transform[] enemySpawns;
    private Transform enemyObjectif;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;

    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void Update()
    {   
        if (!isSpawning) return;
        

        timeSinceLastSpawn += Time.deltaTime;
        /*if (timeSinceLastSpawn >= (1/enemiesPerSecond)&& enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0;
        }*/

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }

    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    public void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        int r_id = Random.Range(0, enemySpawns.Length);
        GameObject newEnemy = Instantiate(prefabToSpawn, enemySpawns[r_id].position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().setTarget(enemyObjectif.position);
    }

    public void SetSpawnPoints(Transform[] spawns)
    {
        enemySpawns = spawns;
    }
    
    public void SetObjectif(Transform objectif)
    {
        enemyObjectif = objectif;
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
