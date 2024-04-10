using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private KeyCode[] keysAttack;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private float attackCooldown;

    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private Animator animator;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerMovement.Control == true)
        {
            foreach (KeyCode key in keysAttack)
            {
                if (Input.GetKeyDown(key) && cooldownTimer > attackCooldown && playerMovement.body.velocity.y == 0)
                {
                    OnAttackAnimation();
                    break;
                }
                else
                if (Input.GetKeyDown(key) && cooldownTimer > attackCooldown && playerMovement.body.velocity.y != 0)
                {
                    animator.SetTrigger("isJumping_Shoot");
                    break;
                }
            }
        }

        cooldownTimer += Time.deltaTime;
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public void OnAttackAnimation()
    {
        if (playerMovement.body.velocity.y == 0)
        {
            playerMovement.Control = false;
            animator.SetTrigger("isAttack");
        }
        else
        if (playerMovement.body.velocity.y != 0)
        {
            animator.SetTrigger("isAttack");
            animator.SetTrigger("isJumping_Shoot");
        }
    }
}