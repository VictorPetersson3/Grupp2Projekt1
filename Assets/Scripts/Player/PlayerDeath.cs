using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 myOriginalPosition;
    private Quaternion myOriginalRotation;

    void Start()
    {
        myOriginalPosition = transform.position;
        myOriginalRotation = transform.rotation;
    }

    public void Die()
    {
        transform.position = myOriginalPosition;
        transform.rotation = myOriginalRotation;
    }
}
