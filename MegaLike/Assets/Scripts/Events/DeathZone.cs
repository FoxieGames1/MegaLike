using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject gameOverUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KillPlayer(other.gameObject);
        }
    }

    private void KillPlayer(GameObject player)
    {
        if (playerHealth != null)
        {
            player.SetActive(false);
            ShowGameOverUI();
        }
    }

    private void Update()
    {
        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            ShowGameOverUI();
        }
    }

    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
