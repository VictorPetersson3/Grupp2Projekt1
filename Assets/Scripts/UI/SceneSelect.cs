using UnityEngine;

public class SceneSelect : MonoBehaviour
{
    GameManager myGameManager = null;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
        {
            StartingTheGame();
        }
    }

    public void StartingTheGame()
    {
        Debug.Log("Laddar \"IntroLevel\"");
        myGameManager.PlayGame();
    }

    public void QuittingTheGame()
    {
        Debug.Log("Quitting the fucking game");
        Application.Quit();
    }
}