using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject myLevelSelect;
    [SerializeField] private GameObject myCreditsScreen;
    [SerializeField] private GameObject myMainMenuObjects;

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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        //Debug.Log("viewing people who worked on this game and also Dave");
        myMainMenuObjects.SetActive(false);
        myCreditsScreen.SetActive(true);
    }

    public void LevelSelect()
    {
        //Debug.Log("viewing levels to select");
        myMainMenuObjects.SetActive(false);
        myLevelSelect.SetActive(true);
    }
    public void LevelOne()
    {
        //Debug.Log("playing level 1");
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
        SceneManager.LoadSceneAsync((int)SceneIndexes.INTROLEVEL, LoadSceneMode.Additive);
    }
    public void LevelTwo()
    {
        //Debug.Log("playing level 2");
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
        SceneManager.LoadSceneAsync((int)SceneIndexes.LEVELTWO, LoadSceneMode.Additive);
    }
    public void LevelThree()
    {
        //Debug.Log("playing level 2");
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU); 
        SceneManager.LoadSceneAsync((int)SceneIndexes.LEVELTHREE, LoadSceneMode.Additive);
    }

    public void BacktoMainMenu()
    {
        //Debug.Log("Returning to main menu");
        myCreditsScreen.SetActive(false);
        myLevelSelect.SetActive(false);
        myMainMenuObjects.SetActive(true);
        }
}
