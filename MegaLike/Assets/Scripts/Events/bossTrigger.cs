using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject bossUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossUI.SetActive(true);
        }
    }
}
