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
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Destroy(gameObject);

        }
    }

    private void Update()
    {
        transform.position += lookAtRotation.forward * 0.1f;
    }
}
