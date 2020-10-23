using UnityEngine;

public class PlayerBackflip : MonoBehaviour
{
    [SerializeField]
    private float myFlipRotationSpeed = 130f;
    [SerializeField]
    private float myBackflipBoostTimeMultiplier = 0.5f;
    [SerializeField]
    private float myCrashAngleTolerance = 45f;
    [SerializeField]
    private PlayerTrickBoost myTrickBoost = null;

    private float myBackflipScore = 0f;

    public void Backflip()
    {
        float rotation = myFlipRotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.left, rotation);
        myBackflipScore += rotation;
    }

    public void GetBackflipScore()
    {
        float boostValue = 0f;

        while (myBackflipScore > 180)
        {
            boostValue += 360;
            myBackflipScore -= 360;
        }
        
        myBackflipScore = 0;
        boostValue *= myBackflipBoostTimeMultiplier;
        myTrickBoost.AddTrickBoostTime(boostValue);
    }

    public bool WillCrash(Vector2 aGroundVector)
    {
        Vector3 playerVector = transform.rotation * -transform.right;
        Vector3 groundvector3 = new Vector3(aGroundVector.x, aGroundVector.y, 0);
        float angle = Vector3.Angle(playerVector, groundvector3);

        if (angle > myCrashAngleTolerance)
        {
            return true;
        }

        return false;
    }
}