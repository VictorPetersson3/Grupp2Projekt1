using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutorialScript : MonoBehaviour
{
    private GameObject myPlayer = null;
    private bool myHaveEntered = false;
    
    [SerializeField] private GameObject myFirstTutorialText = null;

    

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");

        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myFirstTimeCheckOne == false)
        {
            Destroy (this);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && myHaveEntered == true)
        {
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myFirstTimeCheckOne = false;
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision aCollision)
    {
        if (aCollision.gameObject.CompareTag("Player"))
        {
            myHaveEntered = true;
            
            Time.timeScale = 0.1f;
            myFirstTutorialText.SetActive(true);//skapa något, point to world? Fråga Sparky
        }
    }
}
