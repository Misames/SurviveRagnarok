using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private Transform[] enemySpawns;
    private Transform enemyObjectif;
    public float timeBetweenSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;

    public void EnemyDestroyed()
    {
        enemiesAlive -= 1;
    }

    private void Update()
    {
        if (enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn -= 1;
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
        Vector3 spawnPos = enemySpawns[randomSpawn].position;
        GameObject newEnemy = Instantiate(prefabToSpawn, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetTarget(enemyObjectif.position);
        newEnemy.GetComponent<Enemy>().SetEnemyManager(GetComponent<EnemyManager>());
        enemiesAlive++;
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
