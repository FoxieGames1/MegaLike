using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetimeDuration;

    private float direction;
    private float lifetimeTimer;
    private bool hit;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= lifetimeDuration)
        {
            Deactivate();
        }
    }

    public void SetDirection(float _direction)
    {
        lifetimeTimer = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit && collision.CompareTag("Enemy"))
        {
            hit = true;
            Deactivate(); // Desactiva la bala al chocar con un enemigo
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit && collision.gameObject.CompareTag("Wall")) 
        {

        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
