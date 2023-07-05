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
    public float timeVariable;

    public bool isRoundGoing;
    public bool isIntermission;
    public bool isStartOfRound;

    public int round;

    private EnemyManager enemyManager;


    private void Start()
    {
        isRoundGoing = false;
        isStartOfRound = true;
        isIntermission = false;

        timeVariable = Time.time + timeBeforeRoundStarts;

        round = 1;

        enemyManager = GetComponent<EnemyManager>();
        enemyManager.SetSpawnPoints(enemySpawns);
        enemyManager.SetObjectif(Objectif);
    }
    
    private void SpawnEnemies()
    {
        StartCoroutine("ISpawnEnemies");
    } 

    IEnumerator ISpawnEnemies()
    {
        
        enemyManager.SpawnEnemy();
        yield return new WaitForSeconds(1f);
        
    }

    private void Update()
    {
        if (isStartOfRound)
        {
            if (Time.time >= timeVariable)
            {
                isStartOfRound = false;
                isRoundGoing = true;

                SpawnEnemies();
            }

        }

        else if (isIntermission)
        {
            if (Time.time >= timeVariable)
            {
                isIntermission = false;
                isRoundGoing = true;

                SpawnEnemies();
                return;
            }
                

        }

        else if (isRoundGoing)
        {
            /*
            if (Enemies.enemies.Count > 0)
            {

            }

            else
            {*/
            isIntermission = true;
            isRoundGoing = false;

            timeVariable = Time.time + timeBetweenWaves;
            round++;

            //}

        }


    }

}
