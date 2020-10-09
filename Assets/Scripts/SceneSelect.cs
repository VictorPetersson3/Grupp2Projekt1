using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    GameManager myGameManager = null;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
        {
            StartingTheGame();
        }
        /*if (Input.GetKey("escape"))
        {
            QuittingTheGame();
        }*/
    }

    public void StartingTheGame()
    {
        Debug.Log("Laddar \"IntroLevel\"");
        SceneManager.LoadScene(0);
    }

    public void QuittingTheGame()
    {
        Debug.Log("Quitting the fucking game");
        Application.Quit();
    }

}



