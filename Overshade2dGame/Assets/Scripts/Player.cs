using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector2 lastCheckPointPosition;             //stores the last CheckPoint the player has passed
    [SerializeField] private bool playerDead = false;
    [SerializeField] private GameObject healthBarSlider;
    [SerializeField] private LayerMask enemyLayermask;

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
        if (Input.GetKeyDown(KeyCode.B))
        {
            attackEnemy();
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
            dealDamage(GetComponent<PlayerEntity>().maxHealth / 2);

        }

        if (collision.gameObject.CompareTag("death"))
        {
            dealDamage(GetComponent<PlayerEntity>().maxHealth);
        }
    }

    public void dealDamage(float damage)
    {
        GetComponent<PlayerEntity>().health -= damage;
        if (GetComponent<PlayerEntity>().health <= 0) playerDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            //if this checkPoint is further than the prevoius checkpoint | reset cehckpoint
            if(collision.GetComponent<Rigidbody2D>().position.x > lastCheckPointPosition.x)
                lastCheckPointPosition = collision.GetComponent<Rigidbody2D>().position;
        }

        if (collision.gameObject.CompareTag("Dangerous"))
        {
            dealDamage(GetComponent<PlayerEntity>().maxHealth / 2);
        }

        if (collision.gameObject.CompareTag("death"))
        {
            dealDamage(GetComponent<PlayerEntity>().maxHealth);
        }
    }

    private void attackEnemy()
    {
        var enemies = Physics2D.BoxCast(transform.position, GetComponent<BoxCollider2D>().size, 0, Vector2.right, 1.0f, enemyLayermask);
        if(enemies == true)
        {

            enemies.rigidbody.GetComponent<enemy1behavior>().TakeDamage(10);
        }
    }
}
