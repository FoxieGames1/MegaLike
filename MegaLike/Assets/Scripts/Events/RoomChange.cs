using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChange : MonoBehaviour
{
    public void ChangeRoom(int Indice)
    {
        SceneManager.LoadScene(Indice);
    }
}
