using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed ;
    [SerializeField]
    private float health ;
        
    [SerializeField]
    private int killReward ;
    [SerializeField]
    private int damage ;

    private EnemyManager _enemyManager;

    private Vector3 target;

    private void Awake()
    {
    }
    
    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            die();
        }
    }

    public void setTarget(Vector3 position)
    {
        target = position;
    }
    
    public void SetEnemyManager(EnemyManager manager)
    {
        _enemyManager = manager;
    }

    private void die()
    {
        _enemyManager.EnemyDestroyed();
        Destroy(transform.gameObject);
    }

    

    private void Update()
    {
        
        takeDamage(0);
        
    }
}

