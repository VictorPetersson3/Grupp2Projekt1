using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField]
    private GameManager myGameManager = null;
    
    private void OnTriggerEnter(Collider aCollider)
    {
        if (myGameManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            myGameManager.GameFinished(myGameManager.GetActiveScene());
        }
        else
        {
            //GameFinished is only for testing, NextLevel should be used.

            myGameManager.GameFinished(myGameManager.GetActiveScene());
            //myGameManager.NextLevel(myGameManager.GetActiveScene());
        }
    }
}
