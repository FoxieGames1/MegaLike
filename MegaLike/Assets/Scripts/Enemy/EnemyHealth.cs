using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private EnemyMovement enemyMovement;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Vector2 savedPosition;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
            else
            {
                if (animator != null)
                {
                    animator.SetTrigger("Hit");
                }
            }
        }
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Destroy(gameObject);
        }

        if (enemyMovement != null)
        {
            enemyMovement.SetAlive(false);
        }

        savedPosition = transform.position;
        boxCollider.enabled = false;
        rb.simulated = false;
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        boxCollider.enabled = true;
        rb.simulated = true;
        transform.position = savedPosition;

        Destroy(gameObject);
    }
}
