using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float dirX;
    public float speed;
    public float damage;
    public Transform spawnPoint;
    public GameObject parent;

    public void Initialize(float _dirX, float _speed, float _damage, Transform _spawnPoint, GameObject _parent)
    {
        dirX = _dirX;   
        speed = _speed;
        damage = _damage;
        parent = _parent;
        spawnPoint = _spawnPoint;
        transform.position = spawnPoint.position;
    }
   

    public void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(dirX * speed, GetComponent<Rigidbody2D>().velocity.y);
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != parent && collision.tag != "Bullets")
        {
            Destroy(this.gameObject, 0.1f);
        }

        //play particles
    }   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != parent && collision.gameObject.tag != "Bullets")
        {
            Destroy(this.gameObject, 0.1f);
        }

        //play particles
    }

    
}
