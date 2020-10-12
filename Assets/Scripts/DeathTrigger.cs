using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField]
    private GameManager myGameManager = null;

    private void OnTriggerEnter(Collider aCollider)
    {
        myGameManager.GameOver(myGameManager.GetActiveScene());
    }
}