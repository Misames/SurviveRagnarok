using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    [SerializeField]
    private int killReward;
    [SerializeField]
    public uint damage;
    private EnemyManager enemyManager;
    private Vector3 target;

    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) die();
    }

    public void setTarget(Vector3 position)
    {
        target = position;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }

    private void die()
    {
        enemyManager.EnemyDestroyed();
        Destroy(transform.gameObject);
    }
}
