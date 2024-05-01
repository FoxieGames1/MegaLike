using UnityEngine;

public class ProjectileAcceleration : MonoBehaviour
{
    public float accelerationRate = 1f;
    public float maxSpeed = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("El objeto no tiene un Rigidbody2D adjunto.");
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            Vector2 currentDirection = rb.velocity.normalized;

            Vector2 acceleration = currentDirection * accelerationRate * Time.fixedDeltaTime;

            rb.velocity += acceleration;
        }
    }
}
