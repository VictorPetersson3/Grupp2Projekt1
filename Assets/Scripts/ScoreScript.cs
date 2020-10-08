using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public AudioSource myCogSound;

    void OnTriggerEnter(Collider other)
    {
        myCogSound.Play();
        UserInterfaceScript.myCogsCollected += 1;
        Destroy(gameObject);
    }
}
