using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    public uint killReward;
    public int damage;
    private EnemyManager enemyManager;
    private Vector3 target;
    public bool collided;

    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    public void SetTarget(Vector3 position)
    {
        target = position;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }

    private void Die()
    {
        enemyManager.EnemyDestroyed();
        GameManager.Instance.EarnGold(killReward);
        Destroy(transform.gameObject);
    }
}
