using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Camara principal")]
    [Tooltip("Referencia a la cámara principal")]
    [SerializeField] private CinemachineVirtualCamera mainCamera;

    [Header("Camara secundaria")]
    [Tooltip("Referencia a la cámara secundaria")]
    [SerializeField] private CinemachineVirtualCamera secondaryCamera;

    private CinemachineVirtualCamera lastActiveCamera; 

    public void SwitchToSecondaryCamera(CinemachineVirtualCamera newCamera)
    {
        if (lastActiveCamera != null)
        {
            lastActiveCamera.Priority = 0;
        }

        mainCamera.Priority = 0;
        newCamera.Priority = 10;

        lastActiveCamera = newCamera;
    }

    public void SwitchToMainCamera()
    {
        if (lastActiveCamera != null)
        {
            lastActiveCamera.Priority = 0;
        }

        mainCamera.Priority = 10;

        lastActiveCamera = null;
    }
}
