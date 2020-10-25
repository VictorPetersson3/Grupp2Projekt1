using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    [SerializeField]
    private float myDuration = 10f;
    private Player myPlayer = null;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 300, 250, 20), "Invincible active: " + myPlayer.GetInvincible());
        GUI.Label(new Rect(0, 320, 250, 20), "Invincible duration: " + myDuration);
    }

    void Update()
    {
        DeactiveInvincible();
    }

    private void DeactiveInvincible()
    {
        if (myPlayer.GetInvincible())
        {
            myDuration -= Time.deltaTime;
        }
        
        if (myDuration <= 0)
        {
            myPlayer.SetInvincible(false);
            myDuration = 10f;
        }
    }
}
