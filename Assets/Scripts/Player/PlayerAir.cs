using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerAir : MonoBehaviour
{
    //public void AirMovement(float aGravity, ref Vector2 aAirMovement, ref Vector3 aParentPos)
    //{
    //    Vector2 currentMove = Time.deltaTime * aAirMovement;
    //    aParentPos = new Vector3(aParentPos.x + currentMove.x, aParentPos.y + currentMove.y, aParentPos.z);
    //    aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y - (aGravity * Time.deltaTime));

    //    Debug.Log(aParentPos + "Parent Pos");
    //    Debug.Log(aAirMovement + "Air Movement");
    //}

    //public void AirRotation(float aRotationResetSpeed)
    //{
    //    float newXRot = 45f;
    //    float newYRot = 90f;

    //    Quaternion newRot = Quaternion.Euler(newXRot, newYRot, 0);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * aRotationResetSpeed);
    //}

    public void Backflip(float aFlipRotationSpeed)
    {
        transform.Rotate(Vector3.left, aFlipRotationSpeed * Time.deltaTime);
    }
}