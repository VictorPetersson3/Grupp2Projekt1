using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform myTarget = null;
    [SerializeField]
    private float myOffsetXMin = 10f;
    [SerializeField]
    private float myOffsetXMax = 20f;
    [SerializeField]
    private float myClosestZoom = -15f;
    [SerializeField]
    private float myFurthestZoom = -30f;
    [SerializeField]
    private float myOffsetYMultiplier = 1f;
    [SerializeField]
    private float myOffsetYMax = -5f;
    [SerializeField]
    private float myOffsetYResetSpeed = 2f;

    private float myMinPlayerSpeed;
    private float myMaxPlayerSpeed;
    private float myPercent;
    private float myOffsetY = 0f;


    void LateUpdate()
    {
        Vector3 targetPos = myTarget.position;
        targetPos.x = myTarget.position.x + Mathf.Lerp(myOffsetXMin, myOffsetXMax, myPercent);
        targetPos.z = Mathf.Lerp(myClosestZoom, myFurthestZoom, myPercent);
        targetPos.y += myOffsetY;
        
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

    public void UpdateYOffset(float anOffset)
    {
        if (anOffset < 0)
        {
            myOffsetY += anOffset * myOffsetYMultiplier * Time.deltaTime;
            if (myOffsetY < myOffsetYMax)
            {
                myOffsetY = myOffsetYMax;
            }

            return;
        }

        if (myOffsetY < 0)
        {
            myOffsetY += Time.deltaTime * myOffsetYResetSpeed;
            return;
        }

        myOffsetY = 0;
    }
}