using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision aCollision)
    {
        Debug.Log("Collision");
    }
}
