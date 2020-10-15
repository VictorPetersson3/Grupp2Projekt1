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
            myGameManager.NextLevel(myGameManager.GetActiveScene());
        }
    }
}
