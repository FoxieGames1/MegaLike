using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private EnemyMovement enemyMovement; // Referencia al componente EnemyMovement
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public GameObject UILife;
    public GameObject Life;
    private Image healthBar;
    private Vector2 savedPosition;
    [SerializeField] private float TimeToRespectDie;
    [SerializeField] private float fadeDuration;
    [SerializeField] private bool isboss;

    public int CurrentHealth { get { return currentHealth; } }
    public int MaxHealth { get { return maxHealth; } }

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>(); // Obtener referencia al componente EnemyMovement
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (UILife != null)
        {
            healthBar = UILife.GetComponent<Image>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHealth--;
            AudioManager.Instance.PlaySFX("ImpactBow");
            if (healthBar != null)
            {
                healthBar.fillAmount = (float)currentHealth / maxHealth;
            }

            if (currentHealth <= 0)
            {
                DieEnemy();
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

    public void DieEnemy()
    {
        Life.SetActive(false);
        if (animator != null || isboss)
        {
            animator.SetTrigger("Die");
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Destroy(gameObject);
        }

        if (UILife != null)
        {
            UILife.SetActive(false);
        }

        if (enemyMovement != null)
        {
            enemyMovement.SetAlive(false);
            enemyMovement.SetStopped(true);
        }

        savedPosition = transform.position;
        boxCollider.enabled = false;
        rb.simulated = false;
    }

    private IEnumerator DestroyAfterAnimation()
    {
        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0.0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);

        yield return new WaitForSeconds(TimeToRespectDie);
        boxCollider.enabled = true;
        rb.simulated = true;
        transform.position = savedPosition;
        Destroy(gameObject);
    }
}
