using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAHchine : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.B)) 
        {
            anim.SetTrigger("shot");
            shoot();
        
        }
        
    }

    public void shoot() 
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    
    
    }
}
