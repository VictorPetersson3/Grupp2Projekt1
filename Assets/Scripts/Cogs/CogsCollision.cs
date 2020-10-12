using UnityEngine;

public class CogsCollision : MonoBehaviour
{
    private Player myPlayer = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (myPlayer == null)
        {
            Debug.LogError("Error: myPlayer " + myPlayer);
        }
    }
    private void OnCollisionEnter(Collision aCollision)
    {
        myPlayer.IncreaseScore();
        Destroy(gameObject);
    }
}
