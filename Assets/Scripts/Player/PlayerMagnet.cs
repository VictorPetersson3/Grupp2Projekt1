using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField]
    private float myMaxDuration = 15f;
    private float myDuration = 0f;
    private Player myPlayer = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (myPlayer == null)
        {
            Debug.LogError("Error: myPlayer " + myPlayer);
        }
    }

    private void Update()
    {
        DeactivateMagnet();
    }

    private void DeactivateMagnet()
    {
        Debug.Log("Magnet is active!");
        myDuration += Time.deltaTime;
        if (myDuration >= myMaxDuration)
        {
            myPlayer.SetMagnet(false);
            myDuration = 0;
        }
    }
}
