using UnityEngine;

public class Explode : MonoBehaviour
{
    private Player myPlayer = null;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        if (myPlayer.GetInvincible())
        {
            Debug.Log("Should explode");
            //CreateExplosion();
        }

    }

    private void CreateExplosion()
    {

    }
}
