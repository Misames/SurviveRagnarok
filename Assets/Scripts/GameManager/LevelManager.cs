using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    
    public Transform Objectif;
    public Transform[] enemySpawns ;
    
    
    public float timeBetweenWaves;
    public float timeBeforeRoundStarts;
    private float timeVariable;

    private bool isRoundGoing;
    private bool isIntermission;
    private bool isStartOfRound;
    private bool isLevelFinished;

    public int[] enemiesPerRound;

    private int currentRound;

    private EnemyManager enemyManager;


    private void Start()
    {
        isRoundGoing = false;
        isStartOfRound = true;
        isIntermission = false;
        isLevelFinished = false;

        timeVariable = Time.time + timeBeforeRoundStarts;

        currentRound = 0;

        enemyManager = GetComponent<EnemyManager>();
        enemyManager.SetSpawnPoints(enemySpawns);
        enemyManager.SetObjectif(Objectif);
    }
    
    private void SpawnEnemies(int i)
    {
        enemyManager.SpawnEnemies(i);
    } 


    private void Update()
    {

        if (isStartOfRound)
        {
            if (Time.time >= timeVariable)
            {
                isStartOfRound = false;
                isRoundGoing = true;
                SpawnEnemies(enemiesPerRound[currentRound]); ;

            }

        }

        else if (isIntermission )
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
                    isLevelFinished = true;
                    isIntermission = false;
                    isRoundGoing = false;
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