using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformtrap : MonoBehaviour
{
    // Start is called before the first frame update
   
    private float TimeElapsed = 0;
    private float DownTimeElapsed = 0;
    public float delay;
    private Animator animator;
    private float disabledDelay =1.5f;
    bool StartTimer = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTimer) 
        {
            animator.SetTrigger("inactive");
            TimeElapsed += Time.deltaTime;
            if(TimeElapsed > delay) 
            {
                GetComponent<BoxCollider2D> ().enabled = false;
                StartTimer = false;
            
            }
        
        }
        
        if (GetComponent<BoxCollider2D>().enabled == false)
        {
            DownTimeElapsed += Time.deltaTime;
            if (DownTimeElapsed > disabledDelay)
            {
                GetComponent<BoxCollider2D>().enabled = true;
               

            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player") 
        {
            
            StartTimer = true;
        }
    }
}
