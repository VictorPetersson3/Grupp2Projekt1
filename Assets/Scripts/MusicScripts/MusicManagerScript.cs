using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip myMenuMusicClip00 = null;
    [SerializeField]
    private AudioClip myScene01MusicClip = null;
    [SerializeField]
    private AudioClip myScene02MusicClip = null;
    [SerializeField]
    private AudioClip myScene03MusicClip = null;
    [SerializeField]
    private AudioClip myScene03WinMusic = null;

    private AudioSource myMusicSource;
    private Player myPlayer = null;
    private bool myIsFinished = false;

    void Start()
    {
        myMusicSource = GetComponent<AudioSource>();
        if (myMusicSource == null)
        {
            Debug.LogError("Music Audio source is NULL. ");
        }
    }

    void Update()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetActiveScene().isLoaded && SceneManager.GetSceneAt(i) != SceneManager.GetSceneByName("GameManagerScene"))
            {
                if (SceneManager.GetActiveScene().name == "MainMenuScene" && myMusicSource.clip != myMenuMusicClip00)
                {
                    myMusicSource.clip = myMenuMusicClip00;
                    myMusicSource.Play();
                }
                if (SceneManager.GetActiveScene().name == "Level01" && myMusicSource.clip != myScene01MusicClip)
                {
                    myMusicSource.clip = myScene01MusicClip;
                    myMusicSource.Play();
                }
                if (SceneManager.GetActiveScene().name == "Level02" && myMusicSource.clip != myScene02MusicClip)
                {
                    myMusicSource.clip = myScene02MusicClip;
                    myMusicSource.Play();
                }
                if (SceneManager.GetActiveScene().name == "Level3" && myMusicSource.clip != myScene03MusicClip)
                {
                    myMusicSource.clip = myScene03MusicClip;
                    myMusicSource.Play();
                }
            }

        }
    }
}