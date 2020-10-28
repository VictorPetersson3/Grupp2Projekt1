using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenScript : MonoBehaviour
{
    [SerializeField]
    GameObject myContinue = null;
    [SerializeField]
    private AudioClip myScene03WinMusic = null;
    private AudioSource myMusicSource;
    GameManager myGameManager = null;

    private void Start()
    { 
        myMusicSource = GetComponent<AudioSource>();  
        myMusicSource.clip = myScene03WinMusic;
        myMusicSource.Play();  

        myGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (myGameManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            myContinue.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void Continue()
    {
        myGameManager.myLevelComplete = false;
        Time.timeScale = 1f;
        myGameManager.NextLevel(myGameManager.GetActiveScene());
    }

    public void Replay()
    {
        myGameManager.myLevelComplete = false;
        Time.timeScale = 1f;
        myGameManager.ReplayLevel(myGameManager.GetActiveScene());
    }

    public void Home()
    {
        myGameManager.myLevelComplete = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
