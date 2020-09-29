using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerJump : MonoBehaviour
{
    public void Jump(Vector2[] aCurrentPoints, int aPointsIndex, float aJumpForce, float aBaseSpeed, ref Vector2 aAirMovement)
    {
        if (aPointsIndex == 0)
        {
            aAirMovement = aCurrentPoints[0] - aCurrentPoints[1];
        }
        else
        {
            aAirMovement = aCurrentPoints[aPointsIndex] - aCurrentPoints[aPointsIndex - 1];
        }

        aAirMovement = aAirMovement.normalized * aBaseSpeed / 10;
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y + aJumpForce);
    }
}