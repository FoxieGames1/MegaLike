using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private bool movingRight = true;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public float rayDistance = 0.2f;
    public float rayDistanceEnemy = 0.3f;
    public float groundRayDistance = 0.3f;
    public Transform leftDetectionPoint;
    public Transform rightDetectionPoint;
    public Transform groundDetectionPointLeft;
    public Transform groundDetectionPointRight;
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private bool isStopped = false;
    private bool isAlive = true;
    private bool isWaiting = false;
    [SerializeField] private float resumeTimeMovement;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isStopped)
        {
            if (!DetectWalls())
            {
                Move();
            }

            if (!DetectEnemies())
            {
                Move();
            }
        }
        UpdateAnimation();
    }

    private void Move()
    {
        Vector3 movement = CalculateMovement();
        movement *= speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private Vector3 CalculateMovement()
    {
        return movingRight ? Vector3.right : Vector3.left;
    }

    private bool DetectGroundLeft()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        RaycastHit2D groundHit = Physics2D.Raycast(groundDetectionPointLeft.position, direction, groundRayDistance, groundLayer);
        return groundHit.collider != null;
    }

    private bool DetectGroundRight()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        RaycastHit2D groundHit = Physics2D.Raycast(groundDetectionPointRight.position, direction, groundRayDistance, groundLayer);
        return groundHit.collider != null;
    }

    private bool DetectWalls()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, rayDistance, wallLayer);
        if (wallHit.collider != null)
        {
            movingRight = !movingRight;
            FlipSprite();
            return true;
        }
        return false;
    }

    private bool DetectEnemies()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        RaycastHit2D hitLeft = Physics2D.Raycast(leftDetectionPoint.position, direction, rayDistance, enemyLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rightDetectionPoint.position, direction, rayDistance, enemyLayer);

        if (hitLeft.collider != null || hitRight.collider != null)
        {
            movingRight = !movingRight;
            FlipSprite();
            return true;
        }
        return false;
    }

    void DetectPlayer()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, CalculateMovement(), rayDistanceEnemy, playerLayer);
        if (playerHit.collider != null)
        {
            isStopped = true;
            StartCoroutine(ResumeMovement());
        }
    }

    private IEnumerator ResumeMovement()
    {
        isWaiting = true;
        yield return new WaitForSeconds(resumeTimeMovement);
        isStopped = false;
        isWaiting = false;
    }

    private void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
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

    public void SetStopped(bool stopped)
    {
        isStopped = stopped;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
