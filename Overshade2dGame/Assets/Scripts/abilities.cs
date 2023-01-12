using System.Collections;
using System.Collections.Generic;
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
    public float dashSpeed = 0f;
    public float jumpForce = 0f;
    public float movementSpeed = 0f;
    public float crouchSppeed = 0f;


    public Rigidbody2D rb;
    private Vector2 moveDirection;
    


    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementSpeed * move, rb.velocity.y);
    }

}
