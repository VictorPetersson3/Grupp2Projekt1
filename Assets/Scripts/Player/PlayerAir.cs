using System.Security.Cryptography;
using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerAir : MonoBehaviour
{
    public void AirMovement(float aGravity, ref Vector2 aAirMovement)
    {
        Vector2 currentMove = Time.deltaTime * aAirMovement;
        transform.position = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentMove.y, transform.position.z);
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y - (aGravity * Time.deltaTime));
    }

    public void AirRotation(float aRotationResetSpeed)
    {
        float newXRot = 35f;
        float newYRot = 90f;

        Quaternion newRot = Quaternion.Euler(newXRot, newYRot, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * aRotationResetSpeed);
    }

    public void Backflip(float aFlipRotationSpeed)
    {
         transform.Rotate(Vector3.left, aFlipRotationSpeed * Time.deltaTime);
    }
}