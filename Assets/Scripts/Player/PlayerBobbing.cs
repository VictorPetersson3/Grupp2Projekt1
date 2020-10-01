using UnityEngine;

public class PlayerBobbing : MonoBehaviour
{
    [SerializeField]
    private Transform myArt = null;
    [SerializeField]
    private float myBobStrength = 0.25f;
    [SerializeField]
    private float myBobSpeed = 2f;

    private void Start()
    {
        if (myArt == null)
        {
            Debug.LogError("PlayerBobbing misses an art reference!");
        }
    }

    public void Bob()
    {
        Vector3 floatY = myArt.transform.localPosition;
        floatY.y = (Mathf.Sin(Time.time * myBobSpeed) * myBobStrength + myBobStrength);
        myArt.transform.localPosition = floatY;
    }
}
