using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private bool movingRight = true;
    public LayerMask wallLayer;
    public float rayDistance = 0.1f;
    public float rayDistanceEnemy = 0.3f;
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;


    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAlive)
        {
            Move();
            UpdateAnimation();
        }
    }

    void Move()
    {
        Vector3 movement = CalculateMovement();
        movement *= speed * Time.deltaTime;
        transform.Translate(movement);
        DetectWallsAndObstacles(movement);
    }

    Vector3 CalculateMovement()
    {
        return movingRight ? Vector3.right : Vector3.left;
    }

    void DetectWallsAndObstacles(Vector3 movement)
    {
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, movement.normalized, rayDistance, wallLayer);
        if (wallHit.collider != null)
        {
            movingRight = !movingRight;
            FlipSprite();

        }
    }

    void UpdateAnimation()
    {
        if (Mathf.Abs(speed) > 0.01f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }


    public void SetMoveDirection(Vector2 direction)
    {
        movingRight = direction.x > 0f;
    }

    void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
