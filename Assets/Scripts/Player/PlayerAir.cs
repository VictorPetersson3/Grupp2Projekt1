using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerAir : MonoBehaviour
{

    public void AirRotation(float aRotationResetSpeed, Quaternion anOriginalRotation)
    {
        float newXRot = 45f;
        float newYRot = 90f;

        Quaternion newRot = Quaternion.Euler(newXRot, newYRot, 0);
        transform.rotation = Quaternion.Slerp(anOriginalRotation, newRot, Time.deltaTime * aRotationResetSpeed);
    }

    public void Backflip(float aFlipRotationSpeed)
    {
        transform.Rotate(Vector3.left, aFlipRotationSpeed * Time.deltaTime);
    }
}