using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public CharacterController2D characterController;
    public MovementAbilities move;
    private float value;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        value = characterController.m_JumpForce;
        characterController.m_JumpForce = value * 1.5f;
        move.jumpPrompt = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        characterController.m_JumpForce = value;
    }
}
