using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    private int currentCameraIndex = 0;
    private int lastCameraIndex = 0;
    private int direction = 1;

    private void Start()
    {
        lastCameraIndex = cameras.Length - 1;
    }

    public void SwitchToNextCamera()
    {
        // Desactiva la cámara actual
        cameras[currentCameraIndex].Priority = 0;

        // Calcula el índice de la siguiente cámara
        currentCameraIndex += direction;

        // Verifica si se llegó al final de la lista o al principio
        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = cameras.Length - 1;
            direction = -1; // Cambia la dirección a retroceso
        }
        else if (currentCameraIndex < 0)
        {
            currentCameraIndex = 0;
            direction = 1; // Cambia la dirección a avance
        }

        // Activa la siguiente cámara
        cameras[currentCameraIndex].Priority = 10;
    }

    public void SwitchToLastCamera()
    {
        // Desactiva la cámara actual
        cameras[currentCameraIndex].Priority = 0;

        // Activa la última cámara registrada
        cameras[lastCameraIndex].Priority = 10;

        // Resetea la dirección para el próximo trigger
        direction = 1;
    }
}
