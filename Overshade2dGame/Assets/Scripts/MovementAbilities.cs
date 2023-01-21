using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAbilities : MonoBehaviour
{
    //this class contains all movement attributes of the player
    //EXTRA ATTRs CAN BE ADDED   

    private float directionX = 0f;
    public float movementSpeed = 0f;
    [Range(1,5)]public float dashScale = 0f; //how much to multiply the moveSpeed by (1 maintains the speed, 5 multiplies it by 5)

    private bool jumpPrompt = false;
    private bool crouchPrompt = false;

    private CharacterController2D controller; //an external script for smooth movement

    private void Start()
    {
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
        //dash
        if (Input.GetButtonDown("Dash")) directionX *= dashScale;
        //jump
        if (Input.GetButtonDown("Jump")) jumpPrompt = true;
        //crouch
        if (Input.GetButtonDown("Crouch")) crouchPrompt = true;
        else if (Input.GetButtonUp("Crouch")) crouchPrompt = false;
    }

}
