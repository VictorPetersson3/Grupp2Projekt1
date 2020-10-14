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

    private void Start()
    {
        if (myObstacleParent == null)
        {
            return;
        }

        myObstacles = myObstacleParent.GetComponentsInChildren<BoxCollider>();
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
            SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
        }
    }

    private void Update()
    {
        
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
        //SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(aScene.name, LoadSceneMode.Additive);
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
}