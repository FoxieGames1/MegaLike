using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
