using UnityEngine;

public class PlayerTrickBoost : MonoBehaviour
{
    [SerializeField]
    private float myMaxTrickBoostTime = 10f;
    [SerializeField]
    private float myCurrentTrickBoost = 0f;

#if UNITY_EDITOR 
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 220, 250, 20), "Trick Boost: " + myCurrentTrickBoost);
    }
#endif

    public float GetMaxTrickBoostTime()
    {
        return myMaxTrickBoostTime;
    }

    public float GetTrickBoostTime()
    {
        return myCurrentTrickBoost;
    }

    public void AddTrickBoostTime(float aTime)
    {
        myCurrentTrickBoost += aTime;
        if (myCurrentTrickBoost > myMaxTrickBoostTime)
        {
            myCurrentTrickBoost = myMaxTrickBoostTime;
        }
        else if (myCurrentTrickBoost < 0)
        {
            myCurrentTrickBoost = 0;
        }
    }
}
