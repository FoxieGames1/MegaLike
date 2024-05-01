using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int damageAmount;
    public float damageInterval;
    public Color damageColor;
    public float flashDuration;

    public SpriteRenderer playerSprite;
    public Image healthBar;
    public GameObject gameOverUI;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private bool canTakeDamage = true;
    public float knockbackForce;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount, Vector2 enemyPosition)
    {
        if (canTakeDamage)
        {
            AudioManager.Instance.PlaySFX("SlashEnemy");
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                ShowGameOverUI();
                currentHealth = 0;
                Destroy(gameObject);
            }

            UpdateHealthUI();
            Vector2 knockbackDirection = ((Vector2)transform.position - enemyPosition).normalized;
            playerMovement.ApplyKnockback(knockbackDirection, knockbackForce);
            StartCoroutine(DamageCooldown());
            StartCoroutine(FlashSprite());

        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageInterval);
        canTakeDamage = true;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void Respawn(Vector3 respawnPoint)
    {
        currentHealth = maxHealth;
        transform.position = respawnPoint;
        UpdateHealthUI();
    }

    private IEnumerator FlashSprite()
    {
        Color originalColor = playerSprite.color;
        playerSprite.color = damageColor; 
        yield return new WaitForSeconds(flashDuration);

        playerSprite.color = originalColor;
    }

}
