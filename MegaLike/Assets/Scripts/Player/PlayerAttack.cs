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
                if (Input.GetKeyDown(key) && cooldownTimer > attackCooldown)
                {
                    OnAttackAnimation();
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
        animator.SetTrigger("isAttack");

    }
    private void Shooting()
    {
        animator.SetTrigger("isAttack");
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}