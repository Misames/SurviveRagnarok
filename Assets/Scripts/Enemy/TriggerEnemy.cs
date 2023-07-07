using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    public GameManager gameManager;
    public EnemyManager enemyManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy hit = other.GetComponent<Enemy>();
        gameManager.ReducePlayerHealth(hit.damage);
        Destroy(other.gameObject);
        enemyManager.EnemyDestroyed();
    }
}
