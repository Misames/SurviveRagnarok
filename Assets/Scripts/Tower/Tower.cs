using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float range;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float fireRate;
    private float nextTimeToFire;
    public GameObject currentTarget;

    private void Start()
    {
        nextTimeToFire = Time.time;
    }

    private void updateNearestEnemy()
    {
        if (currentTarget == null)
        {
            int layerMask = 1 << 8;
            RaycastHit2D target = Physics2D.CircleCast(transform.position, range, transform.forward, 0, layerMask);
            if (target.collider != null)
                currentTarget = target.transform.gameObject;
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
