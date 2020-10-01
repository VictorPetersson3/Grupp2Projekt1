using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerAir : MonoBehaviour
{
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