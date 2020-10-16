﻿using UnityEngine;

public class PlayerBackflip : MonoBehaviour
{
    [SerializeField]
    private float myFlipRotationSpeed = 130f;
    [SerializeField]
    private float myBackflipBoostTimeMultiplier = 0.5f;
    [SerializeField]
    private float myMaxTrickBoostTime = 10f;
    [SerializeField]
    private float myCrashAngleTolerance = 45f;

    private float myBackflipScore = 0f;

    public float GetMaxTrickBoostTime()
    {
        return myMaxTrickBoostTime;
    }

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

        return returnValue;
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