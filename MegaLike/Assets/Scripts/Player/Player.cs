using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float jump_speed = 8;
    public float run_speed = 3;
    public bool SemiJump = false;

    Rigidbody2D RB2D;

    public CheckGround Ground;

    // Start is called before the first frame update
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            RB2D.velocity = new Vector2(-run_speed, RB2D.velocity.y);
        }
        else
        if (Input.GetKey("right"))
        {
            RB2D.velocity = new Vector2(run_speed, RB2D.velocity.y);
        }
        else
        {
            RB2D.velocity = new Vector2(0, RB2D.velocity.y);
        }

        if (Input.GetKeyDown("space") && Ground.isGrounded)
        {
            Ground.isGrounded = false;
            SemiJump = false;
            RB2D.velocity = new Vector2(RB2D.velocity.x, jump_speed);
        }

        if (RB2D.velocity.y > jump_speed / 2 && !Ground.isGrounded && SemiJump == false)
        {
            if (Input.GetKeyUp("space"))
            {
                SemiJump = true;
                RB2D.velocity = new Vector2(RB2D.velocity.x, jump_speed * 0.5f);
            }
        }

        if (Ground.isGrounded)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x, 0);
        }
    }
}
