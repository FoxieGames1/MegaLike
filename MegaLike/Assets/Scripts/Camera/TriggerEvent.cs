using Cinemachine;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    public float DBLend;

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
        // Encuentra la c�mara principal de Cinemachine en la escena
        CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();

        if (cinemachineBrain != null)
        {
            cinemachineBrain.m_DefaultBlend.m_Time = DBLend;
            if (DBLend == 1)
            {
                cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
            }
            else
            if (DBLend != 1)
            {
                cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            }
            
        }
        else
        {
            Debug.LogWarning("No se encontr� una c�mara de Cinemachine en la escena.");
        }

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
                CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();

                if (cinemachineBrain != null)
                {
                    cinemachineBrain.m_DefaultBlend.m_Time = DBLend;
                    if (DBLend == 1)
                    {
                        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                    }
                    else
                    if (DBLend != 1)
                    {
                        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                    }
                }
                else
                {
                    Debug.LogWarning("No se encontr� una c�mara de Cinemachine en la escena.");
                }
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
                CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();

                if (cinemachineBrain != null)
                {
                    cinemachineBrain.m_DefaultBlend.m_Time = 0.8f;
                    if (DBLend == 1)
                    {
                        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                    }
                    else
                    if (DBLend != 1)
                    {
                        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                    }
                }
                else
                {
                    Debug.LogWarning("No se encontr� una c�mara de Cinemachine en la escena.");
                }

                cameraSwitcher.SwitchToMainCamera();
            }
        }
    }
}
