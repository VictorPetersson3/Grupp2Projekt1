using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerJump : MonoBehaviour
{
    public void Jump(Vector2[] aCurrentPoints, int aPointsIndex, float aJumpForce, float aSpeed, ref Vector2 aAirMovement)
    {
        if (aPointsIndex < aCurrentPoints.Length)
        {
            aAirMovement = aCurrentPoints[aPointsIndex + 1] - aCurrentPoints[aPointsIndex];
        }
        else
        {
            aAirMovement = aCurrentPoints[aCurrentPoints.Length] - aCurrentPoints[aCurrentPoints.Length - 1];
        }

        aAirMovement = aAirMovement.normalized * aSpeed / 10;
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y + aJumpForce);
    }
}