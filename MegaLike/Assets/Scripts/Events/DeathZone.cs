using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    
    public Transform respawnPoint;
    public PlayerHealth playerHealth;
    public GameObject gameOverUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        if (playerHealth != null)
        {
            playerHealth.Respawn(respawnPoint.position);
        }
        else
        {
            player.transform.position = respawnPoint.position;
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