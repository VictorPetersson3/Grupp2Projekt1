using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField]
    private GameManager myGameManager = null;
    [SerializeField]
    private GameObject myVictoryScreen = null;
    
    private void OnTriggerEnter(Collider aCollider)
    {
        Time.timeScale = 0f;
        myVictoryScreen.SetActive(true);
    }
}
