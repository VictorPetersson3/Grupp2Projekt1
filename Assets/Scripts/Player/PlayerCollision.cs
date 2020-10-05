using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool myCollision = false;
    private bool myCanCollide = true;

    public bool HasCollided()
    {
        return myCollision;
    }

    public void ResetCollided()
    {
        myCollision = false;
        myCanCollide = true;
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        if (myCanCollide)
        {
            myCollision = true;
            myCanCollide = false;
            aCollision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnCollisionExit(Collision aCollision)
    {
        myCollision = false;
        myCanCollide = true;
    }
}
