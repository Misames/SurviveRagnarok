using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    public EnemyManager enemyManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            GameManager.Instance.ReducePlayerHealth(enemy.damage);
            Destroy(other.gameObject);
            enemyManager.EnemyDestroyed();
        }
    }
}
