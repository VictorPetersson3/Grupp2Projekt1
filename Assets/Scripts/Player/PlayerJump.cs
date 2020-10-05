using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    const float myDivideValue = 10;

    public void Jump(Vector2[] aCurrentPoints, int aPointsIndex, float aJumpForce, float aSpeed, ref Vector2 anAirMovement)
    {
        if (aPointsIndex < aCurrentPoints.Length)
        {
            anAirMovement = aCurrentPoints[aPointsIndex + 1] - aCurrentPoints[aPointsIndex];
        }
        else
        {
            anAirMovement = aCurrentPoints[aCurrentPoints.Length] - aCurrentPoints[aCurrentPoints.Length - 1];
        }

        anAirMovement = anAirMovement.normalized * aSpeed / myDivideValue;
        anAirMovement = new Vector2(anAirMovement.x, anAirMovement.y + aJumpForce);
    }

    public void Bounce(float aJumpForce, float aSpeed, ref Vector2 anAirMovement)
    {
        anAirMovement = anAirMovement.normalized * aSpeed / myDivideValue;
        anAirMovement = new Vector2(anAirMovement.x, anAirMovement.y + aJumpForce);
    }
}