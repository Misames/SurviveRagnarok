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

    public GameObject currentTarget;

    private void Start()
    {
        nextTimeToFire = Time.time;
    }

    private void updateNearestEnemy()
    {
        RaycastHit2D target = Physics2D.CircleCast(transform.position, range, transform.forward, 0);
        Debug.Log(target.collider != null);
        Debug.Log(transform.position);
        if(target.collider!=null){
            currentTarget = target.transform.gameObject;
        }
        else
        {
            currentTarget = null;
        }
    }


    protected virtual void shoot()
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
