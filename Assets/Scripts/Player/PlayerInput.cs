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

    public bool IsQuitting()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }

        return false;
    }

    public bool IsResetting()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            return true;
        }

        return false;
    }
}