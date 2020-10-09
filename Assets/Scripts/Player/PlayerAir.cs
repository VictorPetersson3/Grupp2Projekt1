using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    [SerializeField]
    private float myRotationResetSpeed = 1f;
    [SerializeField]
    private float myCrashAngleTolerance = 45f;

    public void AirMovement(float aGravity, ref Vector2 aAirMovement)
    {
        Vector2 currentMove = Time.deltaTime * aAirMovement;
        transform.position = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentMove.y, transform.position.z);
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y - (aGravity * Time.deltaTime));
    }

    public void AirRotation()
    {
        float newRotX = 0f;
        float newRotY = 90f;
        Quaternion newRot = Quaternion.Euler(newRotX, newRotY, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * myRotationResetSpeed);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, newRot, Time.deltaTime * myRotationResetSpeed);
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