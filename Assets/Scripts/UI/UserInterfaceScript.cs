using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceScript : MonoBehaviour
{
    public GameObject myScoreText;
    public static int myCogsCollected;
    public static int myTotalCogsToCollect;

    void Update()
    {
        myScoreText.GetComponent<Text>().text = myCogsCollected + "/" + myTotalCogsToCollect;
    }

    void Pause()
    {
        Time.timeScale = 0;
    }
}