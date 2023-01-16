using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class abilities : MonoBehaviour
{
    //this class contains all movement attributes of the player
    //all attributes are  turned off by default
    //attributes can be turned by accessing the object script
    //EXTRA ATTRs CAN BE ADDED   
    public bool canRun = false;
    public bool canDash = false;
    public bool canJump = false;
    public bool canMove = false;
    public bool canCrouch = false;

    public float runSpeed = 0f;
    public float dashSpeed = 20.0f;
    public float jumpForce = 0f;
    public float movementSpeed = 0f;
    public float crouchSppeed = 0f;


    public Rigidbody2D rb;
    private Vector2 moveDirection;

    public float dashLength = 0.5f;
    public float dashCooldown = 1f;

    


    void Update()
    {
        move();
        jump();
        dash();
    }


    void move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementSpeed * move, rb.velocity.y);
    }

    void dash()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.C)){
            rb.velocity = new Vector2(dashSpeed * move  , rb.velocity.y);
        }
    }

    void crouch()
    {

    }

    void jump()
    {
        if (Input.GetButton("Jump")) {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

}
