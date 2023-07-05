using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed ;
    [SerializeField]
    private float health ;

    private int killReward ;
    private int damage ;

    private GameObject targetTile;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }
    
    private void Start()
    {
        initializeEnemy();
    }

    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Enemies.enemies.Remove(gameObject);
        EnemyManager.onEnemyDestroy.Invoke();
        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, speed * Time.deltaTime);
    }

 private void checkPosition()
{
    if (targetTile != null && targetTile != MapGenerator.endTile)
    {
        float distance = (transform.position - targetTile.transform.position).magnitude;

        if (distance < 0.01f)
        {
            int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);

            if (currentIndex < MapGenerator.pathTiles.Count - 1)
            {
                targetTile = MapGenerator.pathTiles[currentIndex + 1];
            }
            else
            {
                // Arrivé à la fin du chemin
                targetTile = MapGenerator.endTile;
            }
        }
    }
}

    private void Update()
    {
        checkPosition();
        moveEnemy();
        takeDamage(0);
        
    }
}

