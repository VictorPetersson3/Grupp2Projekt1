using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject myPauseMenu;

    public void Start()
    {
        myPauseMenu.SetActive(false);
    }

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
        }
    }
    public void ResumingTheGame()
    {
        myPauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PausingTheGame()
    {
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myLevelComplete)
        {
            return;
        }
        myPauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.ReplayLevel(gameManager.GetActiveScene());
    }
}