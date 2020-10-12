using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private Player myPlayer = null;
    private Text myScoreText;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myScoreText = GetComponent<Text>();
        if (myPlayer == null)
        {
            Debug.LogError("Error: myPlayer " + myPlayer);
        }
    }

    void Update()
    {
        myScoreText.text = myPlayer.GetScore().ToString();
    }
}
