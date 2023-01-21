using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector2 lastCheckPointPosition;             //stores the last CheckPoint the player has passed
    [SerializeField] private bool playerDead = false;
    [SerializeField] private GameObject healthBarSlider;

    void Start()
    {
        //if the player should die before reaching a checkPoint, he will respawn at his start position
        lastCheckPointPosition = GetComponent<Rigidbody2D>().position;
        healthBarSlider.GetComponent<Slider>().value = 1;
    }

    void Update()
    {
        if (playerDead)
        {
            respawn();
            return;
        }
        healthBarSlider.GetComponent<Slider>().value = GetComponent<PlayerEntity>().health / GetComponent<PlayerEntity>().maxHealth;
    }

    void respawn()
    {
        //respawn the player at it's last checkpoint
        GetComponent<Rigidbody2D>().position = lastCheckPointPosition;
        GetComponent<PlayerEntity>().health = GetComponent<PlayerEntity>().maxHealth;
        playerDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dangerous"))
        {
            //this line is only for demonstration, the reducetion value should be stored in a proper variable
            GetComponent<PlayerEntity>().health -= GetComponent<PlayerEntity>().maxHealth / 2;
            if (GetComponent<PlayerEntity>().health <= 0) playerDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            //if this checkPoint is further than the prevoius checkpoint | reset cehckpoint
            if(collision.GetComponent<Rigidbody2D>().position.x > lastCheckPointPosition.x)
                lastCheckPointPosition = collision.GetComponent<Rigidbody2D>().position;
        }
    }
}
