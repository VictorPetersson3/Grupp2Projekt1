using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public void Jump(Vector2[] aCurrentPoints, int aPointsIndex, float aJumpForce, float aTotalSpeed, ref Vector2 aAirMovement)
    {
        if (aPointsIndex < aCurrentPoints.Length)
        {
            aAirMovement = aCurrentPoints[aPointsIndex + 1] - aCurrentPoints[aPointsIndex];
        }
        else
        {
            aAirMovement = aCurrentPoints[aCurrentPoints.Length] - aCurrentPoints[aCurrentPoints.Length - 1];
        }

        aAirMovement = aAirMovement.normalized * aTotalSpeed / 10;
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y + aJumpForce);
    }

    public void Bounce(ref Vector2 aAirMovement, float aJumpForce)
    {
        aAirMovement = new Vector2(aAirMovement.x, aJumpForce);
    }
}