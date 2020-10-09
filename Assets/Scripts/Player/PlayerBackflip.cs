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
        myBackflipScore += Time.deltaTime * myBackflipBoostTimeMultiplier;
    }

    public float GetBackflipScore()
    {
        Debug.Log("Get backflip score");
        float returnValue = myBackflipScore;
        myBackflipScore = 0;
        if (returnValue > myMaxTrickBoostTime)
        {
            returnValue = myMaxTrickBoostTime;
        }
        return returnValue;
    }
}
