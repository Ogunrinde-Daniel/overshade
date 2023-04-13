using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public float speed;
    public Transform whoToFollow;
    public GameObject[] lights = new GameObject[5];
    public Vector3[] lightPos = new Vector3[5];

    private int rIndex11 = 0;
    private int rIndex12 = 1;
    private float delay = 0;

    void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lightPos[i] = lights[i].transform.localPosition;
        }

    }

    void Update()
    {
        transform.position = whoToFollow.position;

        delay -= Time.deltaTime;
        if (delay < 0)
        {
            if (swap2Lights(rIndex11, rIndex12))
            {
                rIndex11 = Random.Range(0, 5);
                rIndex12 = Random.Range(0, 5);
                delay = Random.Range(0, 0.5f);
            }
        }

    }


    bool swap2Lights(int index1, int index2)
    {
        lights[index1].transform.localPosition = Vector2.MoveTowards(lights[index1].transform.localPosition, 
            lightPos[index2], speed);        
        
        lights[index2].transform.localPosition = Vector2.MoveTowards(lights[index2].transform.localPosition, 
            lightPos[index1], speed);


        if (lights[index1].transform.localPosition == lightPos[index2] &&
            lights[index2].transform.localPosition == lightPos[index1])
        {
            GameObject temp = lights[index1];
            lights[index1] = lights[index2];
            lights[index2] = temp;

            return true;
        }
        return false;

    }
}
