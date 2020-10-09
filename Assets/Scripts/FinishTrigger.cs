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
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        if (loadedScenes[1].name == "Level3")
        {
            myGameManager.GameFinished(loadedScenes[1]);
        }
        else
        {
            myGameManager.GameFinished(loadedScenes[1]);
            //myGameManager.NextLevel(loadedScenes[1]);
        }
    }
}
