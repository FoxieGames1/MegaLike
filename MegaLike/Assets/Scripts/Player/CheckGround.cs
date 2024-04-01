using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public float groundCheckDistance; // Distancia para el Raycast de verificaci�n de suelo
    public LayerMask groundLayer; // Capa del suelo

    // M�todo para verificar si el jugador est� en el suelo
    public bool IsGrounded()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null)
        {
            Debug.Log("Ground detected: " + hit.collider.gameObject.name);
            return true;
        }
        else
        {
            Debug.Log("No ground detected");
            return false;
        }
    }

}
