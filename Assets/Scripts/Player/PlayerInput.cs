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

    public bool IsQuitting()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }

        return false;
    }
}