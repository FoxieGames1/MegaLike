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

    public float CooldownTimer { get { return cooldownTimer; } set { cooldownTimer = value;}}
    public float AttackCooldown { get {  return attackCooldown; } set {  attackCooldown = value;}}

    public KeyCode[] KeysAttack { get { return keysAttack; } set { keysAttack = value; } }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        foreach (KeyCode key in keysAttack)
        {
            if (Input.GetKeyDown(key) && cooldownTimer > attackCooldown)
            {
                Attack();
                break;
            }
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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

    public void OnAttack()
    {
        animator.SetTrigger("isAttack");
    }

    
}