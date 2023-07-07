using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float range;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float fireRate;
    
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform projectileSource;
    

    private float nextTimeToFire ;

    private GameObject currentTarget;

    private void Start()
    {
        nextTimeToFire = Time.time;
    }

    private void updateNearestEnemy()
    {
        if (currentTarget == null)
        {
            int layerMask = 1 << 8;
            RaycastHit2D target = Physics2D.CircleCast(transform.position, range, transform.forward, 0,layerMask);
            if(target.collider != null)
                currentTarget = target.transform.gameObject;
        }
    }
    
    protected virtual void shoot()
    {
        projectileSource.LookAt(currentTarget.transform.position);
        GameObject newBullet = Instantiate(bullet, projectileSource.position, Quaternion.identity);
        
        newBullet.GetComponent<Bullet>().damage = damage;
        newBullet.GetComponent<Bullet>().lookAtRotation = projectileSource.transform;

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
