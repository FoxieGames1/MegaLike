using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public LayerMask targetLayers;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mueve el proyectil en la dirección especificada
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
        {
            // Destruir el proyectil al colisionar con un objeto de las capas objetivo
            Destroy(gameObject);
        }
    }
}
