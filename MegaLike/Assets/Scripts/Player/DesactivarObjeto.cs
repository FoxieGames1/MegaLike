using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarObjeto : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verifica si el collider es del jugador
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerMovement.isGrounded() && PlayerMovement.TouchGrass == true)
            {
                // Desactiva el objeto
                gameObject.SetActive(false);
                print("Set TouchGrass in false");
                PlayerMovement.TouchGrass = false;
            }
        }
    }
}
