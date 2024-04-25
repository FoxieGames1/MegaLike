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

        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        if (cameraSwitcher == null)
        {
            Debug.LogError("No se encontr� el componente CameraSwitcher en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
        if (other.CompareTag("Player"))
        {
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
