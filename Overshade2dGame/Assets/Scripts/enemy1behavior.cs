using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class enemy1behavior : MonoBehaviour
{
    public Transform leftboundary;
    public Transform rightboundary;
    public float speed;
    public float hitpoint;

    public float health;    //don't set a value in the unity editor | set the maxHealth instead
    public float maxHealth;

    private int dirX = 1;
    private int velX = 1;
    private Rigidbody2D rb;
    private bool m_FacingRight = true;
    private Animator animator;
    public bool targetInRange = false;
    [SerializeField]private GameObject target;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("walking", true);

        health = maxHealth;
    }

    void FixedUpdate()
    {
        if (targetInRange)
        {
            animator.SetBool("walking", false);
            animator.SetTrigger("attack");
            target.GetComponent<Player>().dealDamage(hitpoint);
            velX = 0;
        }
        else
        {
            velX = 1;
            animator.SetBool("walking", true);

        }

        var bodyPos = transform.position;
        if (bodyPos.x >= rightboundary.position.x)
        {
            dirX *= -1;
            Flip();
        }
        else if (bodyPos.x <= leftboundary.position.x)
        {
            dirX *= -1;
            Flip();
        }
        rb.velocity = new Vector2 (dirX * speed * Time.deltaTime * velX, rb.velocity.y);

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetBool("walking", false);
            animator.SetTrigger("death");
            Invoke(nameof(deletePlayer), 1.0f);
        }
        else {
            animator.SetTrigger("hit");
        }

    }
    private void deletePlayer() { 
        Destroy(gameObject);
    }

}
