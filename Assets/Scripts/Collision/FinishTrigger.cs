using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject myVictoryScreen = null;
    
    private void OnTriggerEnter(Collider aCollider)
    {
        if (aCollider.transform.parent.tag != "PlayerTag")
        {
            return;
        }

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myLevelComplete = true;
        Player player = aCollider.transform.GetComponentInParent<Player>();
        player.LevelComplete();
        myVictoryScreen.SetActive(true);
    }
}
