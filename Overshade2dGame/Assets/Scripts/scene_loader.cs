using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scene_loader : MonoBehaviour
{
    public void loadscene() 
    {
        SceneManager.LoadScene("leve_concept");

    }

    public void loadScene(int no)
    {
        SceneManager.LoadScene(no);
    }


}
