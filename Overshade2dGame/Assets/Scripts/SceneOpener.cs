using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    private void callLoad()
    {
        gameObject.AddComponent<scene_loader>();
        gameObject.GetComponent<scene_loader>().loadScene(2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Time for boss fight");
        Invoke(nameof(callLoad), 2f);
    }

}
