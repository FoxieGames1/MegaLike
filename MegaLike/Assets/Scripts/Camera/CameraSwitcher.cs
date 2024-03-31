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
        // Desactiva la c�mara actual
        cameras[currentCameraIndex].Priority = 0;

        // Calcula el �ndice de la siguiente c�mara
        currentCameraIndex += direction;

        // Verifica si se lleg� al final de la lista o al principio
        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = cameras.Length - 1;
            direction = -1; // Cambia la direcci�n a retroceso
        }
        else if (currentCameraIndex < 0)
        {
            currentCameraIndex = 0;
            direction = 1; // Cambia la direcci�n a avance
        }

        // Activa la siguiente c�mara
        cameras[currentCameraIndex].Priority = 10;
    }

    public void SwitchToLastCamera()
    {
        // Desactiva la c�mara actual
        cameras[currentCameraIndex].Priority = 0;

        // Activa la �ltima c�mara registrada
        cameras[lastCameraIndex].Priority = 10;

        // Resetea la direcci�n para el pr�ximo trigger
        direction = 1;
    }
}
