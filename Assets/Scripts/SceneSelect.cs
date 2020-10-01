using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.Collections; ---> Vet inte om den här behövs tbh

public class SceneSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
        {
            StartingTheGame();
        }
        if (Input.GetKey("escape"))
        {
            QuittingTheGame();
        }

    }

    public void StartingTheGame()
    {

        Debug.Log("Laddar \"CollisionTestScene\"");
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));

    }

    public void QuittingTheGame()
    {
        Debug.Log("Quitting the fucking game");
        Application.Quit();
    }

}



