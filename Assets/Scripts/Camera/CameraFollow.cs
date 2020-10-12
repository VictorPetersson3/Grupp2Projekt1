using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform myTarget = null;
    [SerializeField]
    private float myOffsetMin = 10f;
    [SerializeField]
    private float myOffsetMax = 20f;
    [SerializeField]
    private float myClosestZoom = -15f;
    [SerializeField]
    private float myFurthestZoom = -30f;

    private float myMinPlayerSpeed;
    private float myMaxPlayerSpeed;
    private float myPercent;
    private float myOffset;

    void LateUpdate()
    {
        Vector3 targetPos = myTarget.position;
        targetPos.x = myTarget.position.x + Mathf.Lerp(myOffsetMin, myOffsetMax, myPercent);
        targetPos.z = Mathf.Lerp(myClosestZoom, myFurthestZoom, myPercent);
        transform.position = targetPos;
    }

    public void SetPlayerSpeeds(float aMinSpeed, float aMaxSpeed)
    {
        myMinPlayerSpeed = aMinSpeed;
        myMaxPlayerSpeed = aMaxSpeed;
    }

    public void CameraZoom(float aSpeed)
    {
        myPercent = (aSpeed - myMinPlayerSpeed) / (myMaxPlayerSpeed - myMinPlayerSpeed);
    }
}