using UnityEngine;

public class PlayerBackflip : MonoBehaviour
{
    [SerializeField]
    private float myFlipRotationSpeed = 130f;
    [SerializeField]
    private float myBackflipBoostTimeMultiplier = 0.5f;
    [SerializeField]
    private float myMaxTrickBoostTime = 10f;

    private float myBackflipScore = 0f;

    public void Backflip()
    {
        float rotation = myFlipRotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.left, rotation);
        myBackflipScore += rotation;
    }

    public float GetBackflipScore()
    {
        float returnValue = 0f;

        while (myBackflipScore > 180)
        {
            returnValue += 360;
            myBackflipScore -= 360;
        }
        
        myBackflipScore = 0;
        returnValue *= myBackflipBoostTimeMultiplier;

        if (returnValue > myMaxTrickBoostTime)
        {
            returnValue = myMaxTrickBoostTime;
        }

        return returnValue;
    }

    public void ResetScore()
    {
        myBackflipScore = 0f;
    }
}
