using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndexes
{
    MANAGER = 0,
    MAIN_MENU = 1,
    INTROLEVEL = 2,
    LEVELTWO = 3,
    LEVELTHREE = 4,
    PAUSE = 5,
    GAME_OVER = 6,
    FINISHED = 7,
    LOADING = 8
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform myObstacleParent = null;
    [SerializeField]
    private BoxCollider[] myObstacles;
    //[SerializeField]
    //private AudioClip myMenuMusicClip;
    //[SerializeField]
    //private AudioClip myCoinCollectClip;
    //[SerializeField]
    //private float myFadeTime = 0.5f;

    //private AudioSource myMusicSource;

    //private float myMusicVolume = 1f;

    //private bool myFadeUp = false;
    //private bool myFadeDown = false;

    private void Start()
    {
        if (myObstacleParent == null)
        {
            return;
        }

        myObstacles = myObstacleParent.GetComponentsInChildren<BoxCollider>();

        //[SerializeField] private MusicManagerScript myMusicManager;  //Elf

    }

    public void ResetObstacles()
    {
        for (int i = 0; i < myObstacles.Length; i++)
        {
            myObstacles[i].enabled = true;
        }
    }

    private void Awake()
    {
        if (SceneManager.sceneCount <= 1)
        {
            //myMusicSource = GetComponent<AudioSource>();
            SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
        }

        //if (myMusicSource == null)
        //{
        //    myMusicSource = gameObject.AddComponent<AudioSource>(); //myMusicSource = gameObject.AddComponent<AudioSource>();
        //}
        //if (myMusicSource == null)
        //{
        //    Debug.LogError("Music Audio source is NULL. ");
        //}
        //else
        //{
        //    myMusicVolume = 1f;
        //    myMusicSource.clip = myMenuMusicClip;
        //}
        //if (!myMusicSource.isPlaying)
        //{
        //    PlayMenuMusic();
        //}
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    PlayMenuMusic();
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    StopMenuMusic();
        //}

        //if (myFadeUp)
        //{
        //    myMusicVolume += Time.deltaTime / myFadeTime;
        //    if (myMusicVolume > 1f)
        //    {
        //        myMusicVolume = 1;
        //        myFadeUp = false;
        //    }
        //    myMusicSource.volume = myMusicVolume;
        //}

        //if (myFadeDown)
        //{
        //    myMusicVolume -= Time.deltaTime / myFadeTime;
        //    if (myMusicVolume <= 0.01f)
        //    {
        //        myMusicVolume = 0;
        //        myMusicSource.Stop();
        //        myFadeDown = false;
        //    }
        //    myMusicSource.volume = myMusicVolume;
        //}
    }

    public void MainMenu(Scene aScene)
    {
        SceneManager.UnloadSceneAsync(aScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    public void PlayGame()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
        SceneManager.LoadSceneAsync((int)SceneIndexes.INTROLEVEL, LoadSceneMode.Additive);
    }

    public void NextLevel(Scene aCurrentScene)
    {
        int nextScene = aCurrentScene.buildIndex + 1;

        SceneManager.UnloadSceneAsync(aCurrentScene);
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
    }

    public void Pause(Scene aScene)
    {

    }

    public void GameOver(Scene aScene)
    {
        SceneManager.UnloadSceneAsync(aScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    public void GameFinished(Scene aScene)
    {
        SceneManager.UnloadSceneAsync(aScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public Scene GetActiveScene()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i) != SceneManager.GetSceneByName("GameManagerScene"))
            {
                return SceneManager.GetSceneAt(i);
            }
        }

        return SceneManager.GetSceneAt(0);
    }

    //public void PlayMenuMusic()
    //{
    //    myMusicSource.Play();
    //    myFadeUp = true;
    //}
    //public void StopMenuMusic()
    //{
    //    myFadeDown = true;
    //}


}