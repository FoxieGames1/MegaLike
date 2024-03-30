using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera Camera_1;
    [SerializeField] public CinemachineVirtualCamera Camera_2;
    [SerializeField] bool SiguienteCamara = false;

    private void Start()
    {
        Debug.Log(SiguienteCamara);
        if (SiguienteCamara)
        {
            Camera_1.Priority = 10;
            Camera_2.Priority = 0;
        }
    }

    public void SwitchCamera()
    {
        SiguienteCamara = true;
        Camera_1.Priority = 0;
        Camera_2.Priority = 10;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SwitchCamera();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        SwitchCameraPrincipa();
    }

    public void SwitchCameraPrincipa()
    {
        Camera_1.Priority = 10;
        Camera_2.Priority = 0;
    }
}
