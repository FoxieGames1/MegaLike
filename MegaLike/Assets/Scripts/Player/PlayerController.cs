using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GroundDetector groundDetector;

    private Rigidbody2D rb; // Referencia al componente Rigidbody2D

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Asignar el componente Rigidbody2D una vez al inicio
    }

    private void Update()
    {
        Move();
        Jump();
        Shoot();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && groundDetector.IsGrounded())
        {
            Debug.Log("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectileObject.transform.right = transform.right;
            }
        }
    }

}
