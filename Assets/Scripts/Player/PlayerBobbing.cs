using UnityEngine;

public class PlayerBobbing : MonoBehaviour
{
    [SerializeField]
    private Transform myArtPlaceHolder = null;
    [SerializeField]
    private float myBobStrength = 0.1f;
    [SerializeField]
    private float myBobSpeed = 5f;

    private void Start()
    {
        if (myArtPlaceHolder == null)
        {
            Debug.LogError("PlayerBobbing misses an art reference!");
        }
    }

    public void Bob()
    {
        Vector3 floatY = myArtPlaceHolder.transform.localPosition;
        floatY.y = (Mathf.Sin(Time.time * myBobSpeed) * myBobStrength + myBobStrength);
        myArtPlaceHolder.transform.localPosition = floatY;
    }
}
