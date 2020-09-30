using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform myTarget = null;
    [SerializeField]
    private bool myTrackX = true;
    [SerializeField]
    private bool myTrackY = true;

    void LateUpdate()
    {
        float newX = transform.position.x;
        float newY = transform.position.y;
        float newZ = transform.position.z;

        if (myTrackX)
        {
            newX = myTarget.position.x;
        }

        if (myTrackY)
        {
            newY = myTarget.position.y;
        }

        transform.position = new Vector3(newX, newY, newZ);
    }
}