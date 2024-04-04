using UnityEngine;

public class DeteccionObjetos : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            // Permitir al jugador subir las escaleras
            GetComponent<PlayerMovement>().TouchingStairs = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            // Detener al jugador de subir las escaleras cuando sale del área de las escaleras
            GetComponent<PlayerMovement>().TouchingStairs = false;
        }
    }
}
