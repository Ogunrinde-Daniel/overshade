using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillate : MonoBehaviour
{
   
    public Transform topboundary;
    public Transform bottomboundary;
    public float speed;
    private int dirY = 1;

    void FixedUpdate()
    {
        var bodyPos = transform.position;
        if(bodyPos.y >= topboundary.position.y)
        {
            dirY *= -1;
        }else if(bodyPos.y <= bottomboundary.position.y)
        {
            dirY *= -1;
        }
        bodyPos.y += dirY * speed * Time.deltaTime;
        transform.position = bodyPos;
    }
}
