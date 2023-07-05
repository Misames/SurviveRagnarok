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

    private Vector3 target;

    private void Awake()
    {
    }
    
    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    private void initializeEnemy()
    {
        
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

    private void die()
    {
        Destroy(transform.gameObject);
    }

    

    private void Update()
    {
        
        takeDamage(0);
        
    }
}

