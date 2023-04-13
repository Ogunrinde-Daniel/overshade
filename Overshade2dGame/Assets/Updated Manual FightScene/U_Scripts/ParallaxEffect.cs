using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject whoToFollow;
    [SerializeField] private float parallaxEffect;
    private float whoToFollowLastPos;
    void Start()
    {
        whoToFollowLastPos = whoToFollow.transform.position.x;
    }

    void FixedUpdate()
    {
        float whoToFollowCurrentPos = whoToFollow.transform.position.x;
        float dist = ((whoToFollowCurrentPos - whoToFollowLastPos) * parallaxEffect);
        transform.position = new Vector3(transform.position.x + dist, transform.position.y, transform.position.z);
        whoToFollowLastPos = whoToFollow.transform.position.x;

    }
}
