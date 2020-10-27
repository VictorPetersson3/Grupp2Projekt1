using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField]
    private float myDuration = 10f;
    private Player myPlayer = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer ");
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 260, 250, 20), "Magnet active: " + myPlayer.GetMagnet());
        GUI.Label(new Rect(0, 280, 250, 20), "Magnet duration: " + myDuration);
    }

    private void Update()
    {
        if (myPlayer == null)
        {
            return;
        }
        DeactivateMagnet();
    }

    private void DeactivateMagnet()
    {
        if (myPlayer.GetMagnet())
        {
            myDuration -= Time.deltaTime;
        }

        if (myDuration <= 0)
        {
            myPlayer.SetMagnet(false);
            myDuration = 10f;
        }
    }
}
