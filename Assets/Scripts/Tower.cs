using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float range;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float fireRate;

    private float nextTimeToFire ;

    private GameObject currentTarget;

    private void Start()
    {
        nextTimeToFire = Time.time;
    }

    private void updateNearestEnemy()
    {
        GameObject currentNearestEnemy=null; 

        float distance = Mathf.Infinity;

        foreach (GameObject enemy in Enemies.enemies)
        {
            if (enemy != null)
            {
                float _distance = (transform.position - enemy.transform.position).magnitude;

                if (_distance < distance)
                {
                    distance = _distance;
                    currentNearestEnemy = enemy;
                }
            }
           
        }

        if (distance <= range)
        {
            currentTarget = currentNearestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }


    private void shoot()
    {
        Enemy enemyScript = currentTarget.GetComponent<Enemy>(); 
        enemyScript.takeDamage(damage);
    }

    private void Update()
    {
        updateNearestEnemy();
        if (Time.time >= nextTimeToFire)
        {
            if (currentTarget != null)
            {
                shoot();
                nextTimeToFire = Time.time + fireRate;
            }
        }
    }
}
