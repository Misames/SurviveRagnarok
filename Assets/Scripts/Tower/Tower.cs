using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range;
    public int damage;
    public float fireRate;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform projectileSource;
    private float nextTimeToFire;
    private GameObject currentTarget;

    private void Start()
    {
        nextTimeToFire = Time.time;
    }

    private void UpdateNearestEnemy()
    {
        if (currentTarget != null)
        {
            float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (dist > range) currentTarget = null;
        }

        if (currentTarget == null)
        {
            int layerMask = 1 << 8;
            RaycastHit2D target = Physics2D.CircleCast(transform.position, range, transform.forward, 0, layerMask);
            if (target.collider != null)
                currentTarget = target.transform.gameObject;
        }
    }

    protected virtual void Shoot()
    {
        projectileSource.LookAt(currentTarget.transform.position);
        GameObject newBullet = Instantiate(bullet, projectileSource.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().damage = damage;
        newBullet.GetComponent<Bullet>().lookAtRotation = Instantiate(projectileSource.transform);
    }

    private void Update()
    {
        UpdateNearestEnemy();
        if (Time.time >= nextTimeToFire)
        {
            if (currentTarget != null)
            {
                Shoot();
                nextTimeToFire = Time.time + fireRate;
            }
        }
    }
}
