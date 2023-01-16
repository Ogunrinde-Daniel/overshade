using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 lastCheckPointPosition;             //stores the last CheckPOint the player has passed
    [SerializeField] private bool gameOver = false;     //if the player has died

    void Start()
    {
        //if the p[layer should die before reaching a checkPoint, he will respawn at his start position
        lastCheckPointPosition = GetComponent<Rigidbody2D>().position;
    }

    void Update()
    {
        if (gameOver)
        {
            //respawn the player at it's last checkpoint
            GetComponent<Rigidbody2D>().position = lastCheckPointPosition;
            gameOver = false;   //reset the gameOver flag
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            //set the respaewn position if the passses a checkpoint
            lastCheckPointPosition = collision.GetComponent<Rigidbody2D>().position;
        }
    }
}
