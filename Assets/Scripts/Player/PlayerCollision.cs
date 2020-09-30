using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool myCollision = false;

    public bool HasCollided()
    {
        return myCollision;
    }
    private void OnCollisionEnter(Collision aCollision)
    {
        myCollision = true;
    }

    private void OnCollisionExit(Collision aCollision)
    {
        myCollision = false;
    }
}
