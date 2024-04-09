using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] float gravityRBD2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private PlayerAttack playerAttack;
    private float wallJumpCooldown;
    private float horizontalInput;
    public bool Start;
    public bool TouchGrass;

    //Interactuable Stairs
    public bool TouchingStairs;
    [SerializeField] private float stairsSpeed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        HandleStairsInteractuable();
        HandleMovement();
        HandleJump();

        foreach (KeyCode key in playerAttack.KeysAttack)
        {
            if (Input.GetKeyDown(key) && playerAttack.CooldownTimer > playerAttack.AttackCooldown)
            {
                playerAttack.OnAttack();
                break;
            }
        }
    }

    public void HandleMovement()
    {
        if (horizontalInput > 0.01f) transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-1, 1, 1);

        if (horizontalInput != 0 && isGrounded())
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void HandleJump()
    {
        if (wallJumpCooldown > 0.2f)
        {
            if (Start == false)
            {
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            }


            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }

            if (isGrounded())
            {
                if (Start == true)
                {
                    TouchGrass = true;
                    Start = false;
                }
            }

            else body.gravityScale = 7;
            if (Input.GetKeyDown(KeyCode.Z)) Jump();
        }
        else wallJumpCooldown += Time.deltaTime;

        // Manejo de la animación de caída
        if (!isGrounded() && !onWall())
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

    }

    private void HandleStairsInteractuable()
    {
        if (TouchingStairs)
        {
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput > 0)
            {
                body.velocity = new Vector2(body.velocity.x, verticalInput * stairsSpeed);
            }
            else if (verticalInput < 0)
            {
                body.velocity = new Vector2(body.velocity.x, -Mathf.Abs(verticalInput) * stairsSpeed);
            }
            else
            {
                body.velocity = new Vector2(body.velocity.x, 0f);
            }
        }
        else
        {
            body.gravityScale = gravityRBD2;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animator.SetTrigger("isJumping");
            animator.SetBool("isFalling", false);
        }
        else if (!onWall())
        {
            animator.SetBool("isFalling", true);
        }

        if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}