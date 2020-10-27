using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject myLevelSelect = null;
    [SerializeField] private GameObject myCreditsScreen = null;
    [SerializeField] private GameObject myMainMenuObjects = null;
    [SerializeField] 
    private GameObject myLoadingScreen = null;
    [SerializeField]
    private GameManager myGameManager = null;

    //[SerializeField] private MusicManagerScript myMusicManager;;  //Elf
    private MusicManagerScript myMusicManager;   //Elf
                                                 // Start is called before the first frame update
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    void Start()
    {
        ////myMusicManager= GameObject.FindGameObjectsWithTag("MusicManager")[0].GetComponent<MusicManagerScript>();  //Elf
        ////myMusicManager.PlayMusic01();   //Elf

        Time.timeScale = 1f;

        if (myLevelSelect==null)
        {
            Debug.LogError("myLevelSelect is fuckywucky");
        }
        if (myCreditsScreen == null)
        {
            Debug.LogError("myCreditsScreen is fuckywucky");
        }
        if (myMainMenuObjects == null)
        {
            Debug.LogError("myMainMenuObjects is fuckywucky");
        }
        if (myLoadingScreen != null)
        {
            myLoadingScreen.SetActive(false);
        }

        myCreditsScreen.SetActive(false);
        myLevelSelect.SetActive(false);
        myLoadingScreen.SetActive(false);

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

    public void PlayGame()
    {
        myMainMenuObjects.SetActive(false);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.INTROLEVEL, LoadSceneMode.Additive));

        StartCoroutine(LoadAsynchronously());    
    }

    public void LevelSelect()
    {
        //Debug.Log("viewing levels to select");
        myMainMenuObjects.SetActive(false);
        myLevelSelect.SetActive(true);
        ////myMusicManager.PlayMenuMusic();   //Elf
    }
    public void LevelOne()
    {
        //Debug.Log("playing level 1");
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
        SceneManager.LoadSceneAsync((int)SceneIndexes.INTROLEVEL, LoadSceneMode.Additive);

        ////myMusicManager.PlayMusic01();   //Elf
    }
    public void LevelTwo()
    {
        //Debug.Log("playing level 2");
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
        SceneManager.LoadSceneAsync((int)SceneIndexes.LEVELTWO, LoadSceneMode.Additive);
        ////myMusicManager.PlayMusic02();   //Elf
    }
    public void LevelThree()
    {
        //Debug.Log("playing level 2");
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU); 
        SceneManager.LoadSceneAsync((int)SceneIndexes.LEVELTHREE, LoadSceneMode.Additive);
        ////myMusicManager.PlayMusic03();   //Elf
    }

    public void BacktoMainMenu()
    {
        //Debug.Log("Returning to main menu");
        myCreditsScreen.SetActive(false);
        myLevelSelect.SetActive(false);
        myMainMenuObjects.SetActive(true);
        
        ////myMusicManager.PlayMusic();   //Elf
    }

    private IEnumerator LoadAsynchronously()
    { 
        myLoadingScreen.SetActive(true);

        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                

                yield return null;
            }
        }

        myLoadingScreen.SetActive(false);
    }
}
