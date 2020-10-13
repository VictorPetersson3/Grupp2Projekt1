using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Aktivera paus-skärmen.
            //GetComponent<Canvas>;
            Time.timeScale = 0;
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
            }

        }

    }

    public void BackToMenu()
    {
        Debug.Log("Laddar huvudmenyn");
        SceneManager.LoadScene(0);
    }

    public void ReplayLevel()
    {
        Debug.Log("Spelar om banan");
        SceneManager.LoadScene("CollisiontestScene");
    }


}
