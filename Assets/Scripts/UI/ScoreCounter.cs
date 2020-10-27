using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject[] myCogs = null;
    private int myCogsLength;
    private Text myScoreText;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myCogs = GameObject.FindGameObjectsWithTag("Coin");
        myCogsLength = myCogs.Length;
        myScoreText = GetComponent<Text>();
        if (myPlayer == null)
        {
            Debug.LogError("Error: myPlayer missing");
        }
    }

    void Update()
    {
        if (myPlayer == null)
        {
            return;
        }
        myScoreText.text = myPlayer.GetScore().ToString() + "/" + myCogsLength;
    }
}
