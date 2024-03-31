using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    private CameraSwitcher cameraSwitcher;

    private void Start()
    {
        // Obtener una referencia al componente CameraSwitcher
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        if (cameraSwitcher == null)
        {
            Debug.LogError("No se encontr� el componente CameraSwitcher en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador entr� en el trigger
        if (other.CompareTag("Player"))
        {
            // Llamar al m�todo SwitchToNextCamera del CameraSwitcher
            if (cameraSwitcher != null)
            {
                cameraSwitcher.SwitchToNextCamera();
            }
            else
            {
                Debug.LogError("No se encontr� el componente CameraSwitcher.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cameraSwitcher != null)
            {
                cameraSwitcher.SwitchToLastCamera(); // Llama al m�todo SwitchToLastCamera del CameraSwitcher
            }
        }
    }
}
