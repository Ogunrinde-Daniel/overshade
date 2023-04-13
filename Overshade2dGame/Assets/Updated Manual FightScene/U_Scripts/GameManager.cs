using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Soundmanager soundManager;

    public GameObject winScreen;         //winScreen to Display when ame is over
    public GameObject tauntText;         //text object to display taunt at game start    

    public bool winScreenActive;         //stores the activestate of the win Screen
    private CameraShaker cameraShaker;   //store an object of the camera shaker
    void Start()
    {
        //get the camera shake from the main camera
        cameraShaker = FindObjectOfType<CameraShaker>();    
        //taunt the player at the start of the game
        tauntPlayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //reload the scene if the win scene is active 
        if (Input.GetKeyDown(KeyCode.Space) && winScreen.activeSelf)
        {
            SceneManager.LoadScene(1);
        }

    }

    public void setWinScreen(string message)
    {
        /*  set the active scene to true and set message as the parameter */
        winScreenActive = true;
        winScreen.SetActive(true);
        winScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
    }

    public void shakeCamera()
    {
        cameraShaker.shakeCamera = true;
    }

    private void tauntPlayer()
    {
        /*show the taunt message and turn off 5 seconds after*/
        tauntText.SetActive(true);
        tauntText.GetComponent<TextMeshProUGUI>().text = OpeningPhrases.getRandomTaunt();
        Invoke(nameof(turnOffTaunt), 5f);
    }
    
    private void turnOffTaunt()
    {
        tauntText.SetActive(false);
    }

}
