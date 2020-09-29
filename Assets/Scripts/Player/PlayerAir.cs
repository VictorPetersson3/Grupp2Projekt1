﻿using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAir : MonoBehaviour
{
    private Player myPlayer;
    void Start()
    {
        myPlayer = GetComponent<Player>();
    }

    public bool InAir(float aGravity, ref Vector2 aAirMovement)
    {
        Vector2 currentMove = Time.deltaTime * aAirMovement;
        transform.position = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentMove.y, transform.position.z);

        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y - (aGravity * Time.deltaTime));
        if (aAirMovement.y < 0)
        {
            return true;
        }
        return false;
    }

    public void ResetRotation(Quaternion aOriginalRotation, float aRotationResetSpeed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, aOriginalRotation, Time.deltaTime * aRotationResetSpeed);
    }
}
