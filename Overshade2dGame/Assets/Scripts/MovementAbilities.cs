using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MovementAbilities : MonoBehaviour
{
    //this class contains all movement attributes of the player
    //EXTRA ATTRs CAN BE ADDED   

    private float directionX = 0f;
    public float movementSpeed = 0f;
    private bool landed = false;
    [Range(1,5)]public float dashScale = 0f; //how much to multiply the moveSpeed by (1 maintains the speed, 5 multiplies it by 5)
    private enum MovementState { IDLE, RUNNING, JUMPING, FALLING, }
    private MovementState state;
    private bool jumpPrompt = false;
    private bool crouchPrompt = false;
    private Animator animator;
    private Rigidbody2D rb;
    private CharacterController2D controller; //an external script for smooth movement

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>(); 
    }

    private void Update()
    {
        movement();
    }

    private void FixedUpdate()
    {

        //control the player movement
        controller.Move(directionX * Time.fixedDeltaTime, crouchPrompt, jumpPrompt);
        //reset jump
        jumpPrompt = false;
    }


    private void movement()
    {
        //Listeners -- the input prompts can be configured in project settings
        //move 
        directionX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        if (directionX != 0f)
        {

            state = MovementState.RUNNING;

        }
        else
        {
            state = MovementState.IDLE;


        }
        /*if (directionX == 0f && controller.m_Grounded == true)
        {
            animator.SetBool("moving", false);
            animator.SetBool("idling", true);
           

        }*/

        //jump
        if (Input.GetButtonDown("Jump")) jumpPrompt = true;

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.JUMPING;
            

        }
        if (rb.velocity.y < -0.1f)
        {
            state = MovementState.FALLING;



        }

        //Debug.Log(state);
        animator.SetInteger("state", (int)state);
        //crouch
        if (Input.GetButtonDown("Crouch")) crouchPrompt = true;
        else if (Input.GetButtonUp("Crouch")) crouchPrompt = false;

        //dash controls
        if (Input.GetButtonDown("Dash"))
        {
            if (controller.m_FacingRight) 
            {
                controller.Move(800f * Time.fixedDeltaTime, crouchPrompt, jumpPrompt);
                jumpPrompt = false;
            }
            else if (!controller.m_FacingRight)
            {
                controller.Move(-800f * Time.fixedDeltaTime, crouchPrompt, jumpPrompt);
                jumpPrompt = false;
            }
        }
    }

}
