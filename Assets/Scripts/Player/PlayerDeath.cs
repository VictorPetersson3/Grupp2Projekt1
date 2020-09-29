using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 myOriginalPosition;

    void Start()
    {
        myOriginalPosition = transform.position;
    }

    public void Die()
    {
        transform.position = myOriginalPosition;
    }
}
