using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool myCollision = false;

    public bool HasCollided()
    {
        return myCollision;
    }

    public void ResetCollided()
    {
        myCollision = false;
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        myCollision = true;
        aCollision.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
