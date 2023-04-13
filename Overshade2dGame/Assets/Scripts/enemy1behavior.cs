using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements;

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
    private BoxCollider2D box;
    private bool m_FacingRight = true;
    private Animator animator;
    public bool targetInRange = false;
    [SerializeField]private GameObject target;
    [SerializeField] private LayerMask layer;
    public AudioSource hit;

    private bool dead = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("walking", true);
        box = GetComponent<BoxCollider2D>();
        health = maxHealth;
    }

    void FixedUpdate()
    {
        /* RaycastHit hit;
         if (Physics.Raycast(transform.position, transform.right, out hit))
         {
             // Check if the object hit has the "Player" layer or tag
             if (hit.collider.gameObject.CompareTag("Player"))
             {
                 targetInRange = true;
                 Debug.Log("Hit player!");
                 // Do something when the player is hit
             }
             else { targetInRange = false; }
         }*/

        if (dead)
            return;

        if(IsGrounded() == true) { targetInRange = true; } else { targetInRange = false; }
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
            dead = true;
            animator.SetBool("walking", false);
            animator.SetTrigger("death");
            Invoke(nameof(deletePlayer), 1.0f);
        }
        else {
            Debug.Log("owie");
            animator.SetTrigger("hit");
            hit.Play(); 
        }

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet") 
        {
            TakeDamage(10);
           
       
        
        }
    }*/
    private void deletePlayer() {
        Destroy(gameObject);
    }
    private bool IsGrounded()
    {
        if (m_FacingRight)
        {
            RaycastHit2D rayhit = Physics2D.Raycast(box.bounds.center, Vector2.right, box.bounds.extents.x + 1f, layer);
            Color color;
            if (rayhit.collider != null)
            {
                color = Color.green;

            }
            else { color = Color.red; }
            //Debug.DrawRay(box.bounds.center, Vector2.right * (box.bounds.extents.x + 1f), color);
            //Debug.Log(rayhit.collider);
         
            return rayhit.collider != null;
        }
        else
        {
            RaycastHit2D rayhit = Physics2D.Raycast(box.bounds.center, Vector2.left, box.bounds.extents.x + 1f, layer);
            Color color;
            if (rayhit.collider != null)
            {
                color = Color.green;

            }
            else { color = Color.red; }
            //Debug.DrawRay(box.bounds.center, Vector2.left * (box.bounds.extents.x + 1f), color);
            //Debug.Log(rayhit.collider);

            return rayhit.collider != null;
        }
    }

}
