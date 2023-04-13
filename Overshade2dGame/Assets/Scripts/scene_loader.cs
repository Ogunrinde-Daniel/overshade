using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scene_loader : MonoBehaviour
{
    public GameObject secondPart; 
    public GameObject Bg;
    public GameObject firstPart;
    public void loadScene(int no)
    {
        SceneManager.LoadScene(no);
    }


    public void startGame()
    {
        Bg.SetActive(false);
        firstPart.SetActive(false);
        secondPart.SetActive(true);
    }

}
