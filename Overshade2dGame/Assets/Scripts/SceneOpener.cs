using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    public int index = 0;
    private void callLoad()
    {
        gameObject.AddComponent<scene_loader>();
        gameObject.GetComponent<scene_loader>().loadScene(index);
    }

    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Time for boss fight");
        Invoke(nameof(callLoad), 0.5f);
    }

}
