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
        //Vector3 newPos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.deltaTime * myBobSpeed) * Time.timeScale * myBobStrength), transform.position.z);
        //transform.position = newPos;
    }
}
