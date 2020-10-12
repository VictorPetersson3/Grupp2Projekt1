using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttractCogs : MonoBehaviour
{
    [SerializeField]
    private float myAttractReach = 0.1f;
    [SerializeField]
    private float myAttractSpeed = 2f;

    private GameObject[] myCogs;

    private void Start()
    {
        myCogs = GameObject.FindGameObjectsWithTag("Coin");
    }
    private void Update()
    {
        //foreach(GameObject cog in myCogs)
        for (int i = 0; i < myCogs.Length; i++) 
        {
            Attract(myCogs[i].transform);
        }
    }

    private void Attract(Transform aCog)
    {
        if (Vector3.Distance(aCog.transform.position, transform.position) <= myAttractReach)
        {
            Vector3 dir = (transform.position - aCog.transform.position).normalized;
            aCog.transform.position += dir * Time.deltaTime * myAttractSpeed;
        }
    }
}
