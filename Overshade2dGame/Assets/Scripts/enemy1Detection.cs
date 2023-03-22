using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1Detection : MonoBehaviour
{
    
    [SerializeField]private GameObject parentBehaviour;
       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentBehaviour.GetComponent<enemy1behavior>().targetInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentBehaviour.GetComponent<enemy1behavior>().targetInRange = false;
        }
    }
}
