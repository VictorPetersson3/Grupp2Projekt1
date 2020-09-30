using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float myShakeDuration;
    private float myShakeMagnitude;
    private float myDampingSpeed = 1f;
    private float myMultiplier = 0.01f;
    private Vector3 myOriginalPosition;

    private void OnEnable()
    {
        myOriginalPosition = transform.localPosition;
    }

    void Update()
    {
        if (myShakeDuration > 0)
        {
            transform.localPosition = myOriginalPosition + Random.insideUnitSphere * myShakeMagnitude;
            myShakeDuration -= Time.deltaTime * myDampingSpeed;
        }
        else
        {
            myShakeDuration = 0f;
            transform.localPosition = myOriginalPosition;
        }
    }

    public void TriggerShake(float aShakeDuration, float aShakeMagnitude)
    {
        myShakeDuration = (aShakeDuration * myMultiplier);
        myShakeMagnitude = (aShakeMagnitude * myMultiplier);
    }
}
