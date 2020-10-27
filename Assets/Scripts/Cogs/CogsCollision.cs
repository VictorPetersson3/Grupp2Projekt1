﻿using UnityEngine;

public class CogsCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject mySoundContainer = null;
    [SerializeField]
    private GameObject myCogSound = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer");
        }
        if (myCogSound == null)
        {
            Debug.LogError("myCogSound: missing");
        }
    }
    private void OnCollisionEnter(Collision aCollision)
    {
        if (myPlayer == null || myCogSound == null)
        {
            return;
        }

        if (aCollision.collider.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            myPlayer.IncreaseScore();
            Instantiate(myCogSound, mySoundContainer.transform);
            Destroy(gameObject);
        }
    }
}
