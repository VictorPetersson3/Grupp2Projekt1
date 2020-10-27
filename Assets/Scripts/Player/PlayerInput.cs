using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool IsJumping()
    {
        if (Input.GetAxisRaw("Jump") != 0)
        {
            return true;
        }

        return false;
    }

    public bool PressJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            return true;
        }
        return false;
    }
}