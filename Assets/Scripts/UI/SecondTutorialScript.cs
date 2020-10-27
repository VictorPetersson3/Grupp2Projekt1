using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTutorialScript : MonoBehaviour
{
    private GameObject myPlayer = null;
    private bool myHaveEntered = false;
    [SerializeField] private GameObject mySecondTutorialText = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && myHaveEntered == true)
        {
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision aCollision)
    {
        if (aCollision.gameObject.CompareTag("Player"))
        {
            myHaveEntered = true;
            Time.timeScale = 0.1f;
            mySecondTutorialText.SetActive(true);//skapa något, point to world? Fråga Sparky
        }
    }
}
