using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float detectionRange;
    public float attackRange;
    public float attackCooldown;
    public LayerMask playerLayer;
    public int damage = 1;
    public Animator animator;
    public EnemyMovement enemyMovement;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed;

    private Transform player;
    private bool isPlayerInRange = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPlayerInRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            AttackPlayer();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            enemyMovement.SetStopped(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            enemyMovement.SetStopped(false);
        }
    }

    private void AttackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, transform.position);
            }
            Vector2 shootDirection = (player.position - transform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = shootDirection * projectileSpeed;
            }
            animator.SetTrigger("Attack");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
