using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform myObstacleParent = null;
    [SerializeField]
    private BoxCollider[] myObstacles;

    private void Start()
    {
        if (myObstacleParent == null)
        {
            return;
        }

        myObstacles = myObstacleParent.GetComponentsInChildren<BoxCollider>();
    }

    public void ResetObstacles()
    {
        for (int i = 0; i < myObstacles.Length; i++)
        {
            myObstacles[i].enabled = true;
        }
    }
}