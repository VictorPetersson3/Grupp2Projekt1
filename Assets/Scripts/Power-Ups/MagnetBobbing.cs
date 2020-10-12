using UnityEngine;

public class MagnetBobbing : MonoBehaviour
{
    [SerializeField]
    private float myBobStrength = 1f;
    [SerializeField]
    private float myBobSpeed = 5f;

    private void Update()
    {
        Bob();
    }

    private void Bob()
    {
        Vector3 newPos = transform.position;
        newPos.y = transform.position.y + (Mathf.Sin(Time.time * myBobSpeed) * myBobStrength / 10f);
        transform.position = newPos;
    }
}
