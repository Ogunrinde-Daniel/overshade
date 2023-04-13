using UnityEngine;
public class CameraShaker : MonoBehaviour
{
    /*
    This class shakes the camera
    to shake the camera, attach this script to a camera object and set the parameters, 
    then set the bool "shakeCamera" to true from any other script to activate a screen shake
    */

    public Vector2 xShakeRange;
    public Vector2 yShakeRange;

    public float SHAKE_TIME;            //stores the time the camera shake to last
    private float shakeTimer = 0;       //time Passed since camera shake started
    
    public bool shakeCamera = false;    //enable the camerashake
    
    private Vector3 originalPosition;   //this is where the camera will return to after the shake
    private bool up;                    //is the camera currently going up/right
    
    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (shakeCamera)
        {
            shakeTimer += Time.deltaTime;
            if (shakeTimer >= SHAKE_TIME)
            {
                shakeCamera = false;
                transform.position = originalPosition;
                shakeTimer = 0;
            }
            up = !up;
            if (shakeCamera)
            {
                if(up)
                    shake(Random.Range(xShakeRange.x, xShakeRange.y), Random.Range(yShakeRange.x, yShakeRange.y)) ;
                else
                    shake(Random.Range(xShakeRange.x, xShakeRange.y) *-1, Random.Range(yShakeRange.x, yShakeRange.y) *-1);

            }

        }

    }

    void shake(float xIntensity, float yIntensity)
    {
        transform.position = new Vector3(transform.position.x + (xIntensity), transform.position.y + (yIntensity), transform.position.z);
    }

}
