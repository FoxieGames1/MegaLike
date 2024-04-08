using UnityEngine;

public class Points : MonoBehaviour
{
    public int pointValue = 10; // Valor de puntos que otorga esta moneda

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPoints>().AddPoints(pointValue);
            Destroy(gameObject);
        }
    }
}
