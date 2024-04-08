using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private bool movingRight = true;
    public LayerMask wallLayer;
    public LayerMask enemyLayer; 
    public float rayDistance = 0.1f;
    public float rayDistanceEnemy = 0.3f;

    void Update()
    {
        Move();
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
            Debug.Log("Hit wall, changing direction");
            movingRight = !movingRight;
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        movingRight = direction.x > 0f;
    }
}
