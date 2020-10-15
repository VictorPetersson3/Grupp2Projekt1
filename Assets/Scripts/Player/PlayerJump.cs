using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float myJumpForce = 7f;

    public void Jump(Vector2[] aCurrentPoints, int aPointsIndex, float aTotalSpeed, ref Vector2 aAirMovement)
    {
        if (aPointsIndex < aCurrentPoints.Length - 1)
        {
            aAirMovement = aCurrentPoints[aPointsIndex + 1] - aCurrentPoints[aPointsIndex];
        }
        else
        {
            aAirMovement = aCurrentPoints[aCurrentPoints.Length] - aCurrentPoints[aCurrentPoints.Length - 1];
        }

        aAirMovement = aAirMovement.normalized * aTotalSpeed / 10;
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y + myJumpForce);
    }

    public void Bounce(ref Vector2 aAirMovement)
    {
        aAirMovement = new Vector2(aAirMovement.x, myJumpForce);
    }
}