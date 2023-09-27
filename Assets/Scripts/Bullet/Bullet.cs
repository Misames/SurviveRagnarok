using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Transform lookAtRotation;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.collided != true)
            {
                enemy.TakeDamage(damage);
                enemy.collided = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.collided = false;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.position += lookAtRotation.forward * 0.1f;
    }
}
