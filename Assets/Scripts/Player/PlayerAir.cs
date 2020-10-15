using System.Security.Cryptography;
using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerAir : MonoBehaviour
{
    [SerializeField]
    private float myRotationResetSpeed = 1f;

    public Vector3 AirMovement(float aGravity, ref Vector2 aAirMovement)
    {
        Vector2 currentMove = Time.deltaTime * aAirMovement;
        Vector3 oldPosition = transform.position;
        transform.position = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentMove.y, transform.position.z);
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y - (aGravity * Time.deltaTime));
        return oldPosition;
    }

    public void AirRotation()
    {
        float newRotX = 0f;
        float newRotY = 90f;
        Quaternion newRot = Quaternion.Euler(newRotX, newRotY, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * myRotationResetSpeed);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, newRot, Time.deltaTime * myRotationResetSpeed);
    }
}