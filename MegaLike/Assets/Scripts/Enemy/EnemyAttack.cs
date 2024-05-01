using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float detectionRange;
    public float attackRange;
    public LayerMask playerLayer;
    public int damage = 1;
    public Animator animator;
    public EnemyMovement enemyMovement;

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
