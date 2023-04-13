using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private LayerMask enemyLayermask;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb.velocity = transform.right * speed;
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
            return;
        attackEnemy();
        anim.SetTrigger("explode");
        rb.velocity = transform.right * 0;
        Debug.Log("<color = red>Colliding with: " + collision.gameObject.name);
        Invoke(nameof(bulletkill), 0.6f);
       
    }

    private void bulletkill() { Destroy(gameObject); }


    private void attackEnemy()
    {
        var enemies = Physics2D.CircleCast(transform.position, GetComponent<CircleCollider2D>().radius, Vector2.right, 1.0f, enemyLayermask);
        if (enemies == true)
        {

            enemies.rigidbody.GetComponent<enemy1behavior>().TakeDamage(20);
        }
    }
}
