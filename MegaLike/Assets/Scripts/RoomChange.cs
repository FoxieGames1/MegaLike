using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChange : MonoBehaviour
{
    public void ChangeRoom(int Indice)
    {
        SceneManager.LoadScene(Indice);
    }
}
