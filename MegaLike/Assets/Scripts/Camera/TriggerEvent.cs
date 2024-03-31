using Cinemachine;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    [Header("Camera Switcher")]
    [Tooltip("Referencia al componente CameraSwitcher")]
    private CameraSwitcher cameraSwitcher;

    [SerializeField]
    [Header("Camera to Switch")]
    [Tooltip("CinemachineVirtualCamera que se cambiar� al entrar en el trigger")]
    private CinemachineVirtualCamera cameraToSwitch;

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
            // Pasar la referencia de la c�mara secundaria al CameraSwitcher
            if (cameraSwitcher != null && cameraToSwitch != null)
            {
                cameraSwitcher.SwitchToSecondaryCamera(cameraToSwitch);
            }
            else
            {
                Debug.LogError("No se encontr� el componente CameraSwitcher o la c�mara secundaria.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el jugador sali� del trigger
        if (other.CompareTag("Player"))
        {
            // Cambiar a la c�mara principal cuando el jugador salga del trigger
            if (cameraSwitcher != null)
            {
                cameraSwitcher.SwitchToMainCamera();
            }
        }
    }
}
