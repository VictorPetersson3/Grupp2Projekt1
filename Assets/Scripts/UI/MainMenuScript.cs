using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject myLevelSelect;
    public GameObject myCreditsScreen;
    public GameObject myMainMenuObjects;


    // Start is called before the first frame update
    void Start()
    {
        myCreditsScreen.SetActive(false);
        myLevelSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }
    
    void Credits()
    {
        Debug.Log("viewing people who worked on this game and also Dave");
        myMainMenuObjects.SetActive(false);
        myCreditsScreen.SetActive(true);
    }

    void LevelSelect()
    {
        Debug.Log("viewing levels to select");
        myMainMenuObjects.SetActive(false);
        myLevelSelect.SetActive(true);
    }
    void LevelOne()
    {
        Debug.Log("playing level 1");
        SceneManager.LoadScene(2); //Game manager = scen0, main menu = scen1
    }
    void LevelTwo()
    {
        Debug.Log("playing level 2");
        SceneManager.LoadScene(3); //Game manager = scen0, main menu = scen1
    }
    void LevelThree()
    {
        Debug.Log("playing level 2");
        SceneManager.LoadScene(4); //Game manager = scen0, main menu = scen1
    }

    void BacktoMainMenu()
    {
        Debug.Log("Returning to main menu");
        myCreditsScreen.SetActive(false);
        myLevelSelect.SetActive(false);
        myMainMenuObjects.SetActive(true);
        //SceneManager.LoadScene(1);
    }


    //Level Select SetActive??????

    //Ladda scener utifrån objekten

    //Dubbelkolla med LD hur många banor vi har

    //F.a. gåt illbaka till Main -> Load Scene 1?

    //---

    //Credits SetActive???????

    //F.a. gåt illbaka till Main -> Load Scene 1?

    //Skaffa element från Gevik för alla inblandeade



}
