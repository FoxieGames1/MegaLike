using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool isGrounded;

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D Col2D)
    {
        if (Col2D.gameObject.tag == "Solid")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Col2D)
    {
        if (Col2D.gameObject.tag == "Solid")
        {
            isGrounded = false;
        }
    }
}
