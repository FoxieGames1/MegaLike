using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float floatSpeed = 1f; 
    public float floatHeight = 0.5f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        Vector3 newPosition = initialPosition + Vector3.up * offset;
        transform.position = newPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.CollectCollectible();
            AudioManager.Instance.PlaySFX("StarParkle");
            Destroy(gameObject);
        }
    }
}
