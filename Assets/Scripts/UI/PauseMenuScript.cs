using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;  <<-fuck is this shit?
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject myPauseMenu;

    private GameManager myGamemanager;

    public void Start()
    {
        myPauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumingTheGame();
            }
            else
            {
                PausingTheGame();
            }




            //Aktivera paus-skärmen.
            //GetComponent<Canvas>;
            /*Time.timeScale = 0;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
            }*/

        }

    }
    public void ResumingTheGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //Debug.Log("resuming the game");
            myPauseMenu.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        //Debug.Log("resuming the game");
        myPauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PausingTheGame()
    {
        //Debug.Log("pausing the game");
        myPauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    public void BackToMenu()
    {
        Debug.Log("Laddar huvudmenyn");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void ReplayLevel()
    {
        Debug.Log("Spelar om banan");
        //myPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


}
