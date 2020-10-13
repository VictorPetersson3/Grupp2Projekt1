using UnityEngine;

public struct CollisionData
{

    private bool myHasCollided;
    private string myTag;
    private BoxCollider myBoxCollider;

    public CollisionData(bool aHasCollided, string aTag, BoxCollider aBoxCollider)
    {
        myHasCollided = aHasCollided;
        myTag = aTag;
        myBoxCollider = aBoxCollider;
    }

    public void SetHasCollided(bool aHasCollided)
    {
        myHasCollided = aHasCollided;
    }
    public bool GetHasCollided()
    {
        return myHasCollided;
    }
    public void SetTag(string aTag)
    {
        myTag = aTag;
    }   
    public string GetTag()
    {
        return myTag;
    }
    public void SetBoxCollider(BoxCollider aBoxCollider)
    {
        myBoxCollider = aBoxCollider;
    }
    public void InactivateBoxCollider()
    {
        myBoxCollider.enabled = false;
    }
}

public class PlayerCollision : MonoBehaviour
{
    private CollisionData myCollisionData;

    public CollisionData ReturnCollisionData()
    {
        return myCollisionData;
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        myCollisionData.SetHasCollided(true);
        myCollisionData.SetTag(aCollision.gameObject.tag);
    }

    private void OnCollisionExit(Collision aCollision)
    {
        myCollisionData.SetHasCollided(false);
        myCollisionData.SetTag("");
    }
}
