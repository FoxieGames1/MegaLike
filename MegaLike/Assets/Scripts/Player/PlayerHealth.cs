using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Image healthBar;
    public GameObject gameOverUI;
    public int damageAmount;


    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth == 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
            ShowGameOverUI();
        }
        UpdateHealthUI();
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
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damageAmount);
        }
    }
}
