using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackflip : MonoBehaviour
{
    public void Backflip(float aFlipRotationSpeed)
    {
        transform.Rotate(Vector3.left, aFlipRotationSpeed * Time.deltaTime);
    }
}
