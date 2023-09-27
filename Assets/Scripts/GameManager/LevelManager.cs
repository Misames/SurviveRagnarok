using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public TextMeshProUGUI enemy;
    public GameObject grid;
    public GameObject victoryMenu;
    public Transform objectif;
    public Transform[] enemySpawns;
    public float timeBetweenWaves;
    public float timeBeforeRoundStarts;
    private float timeVariable;
    private bool isRoundGoing;
    private bool isIntermission;
    private bool isStartOfRound;
    public int[] enemiesPerRound;
    private int currentRound;
    private EnemyManager enemyManager;

    private void Start()
    {
        isRoundGoing = false;
        isStartOfRound = true;
        isIntermission = false;
        timeVariable = Time.time + timeBeforeRoundStarts;
        currentRound = 0;
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.SetSpawnPoints(enemySpawns);
        enemyManager.SetObjectif(objectif);
    }

    private void SpawnEnemies(int i)
    {
        enemyManager.SpawnEnemies(i);
    }

    private void Update()
    {
        enemy.text = enemyManager.GetEnnemiesAlive().ToString();

        if (isStartOfRound)
        {
            if (Time.time >= timeVariable)
            {
                isStartOfRound = false;
                isRoundGoing = true;
                SpawnEnemies(enemiesPerRound[currentRound]);
            }
        }
        else if (isIntermission)
        {
            if (Time.time >= timeVariable)
            {
                isIntermission = false;
                isStartOfRound = true;
            }
        }
        else if (isRoundGoing)
        {
            if (enemyManager.GetEnnemiesAlive() == 0)
            {
                currentRound++;
                if (currentRound == enemiesPerRound.Length)
                {
                    isIntermission = false;
                    isRoundGoing = false;
                    victoryMenu.SetActive(true);
                    grid.SetActive(false);
                }
                else
                {
                    isIntermission = true;
                    isRoundGoing = false;
                    timeVariable = Time.time + timeBetweenWaves;
                }
            }
        }
    }
}
