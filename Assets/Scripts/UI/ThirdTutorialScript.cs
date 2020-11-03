using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTutorialScript : MonoBehaviour
{
    private GameObject myPlayer = null;
    private bool myHaveEntered = false;
    [SerializeField] private GameObject myThirdTutorialText = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myFirstTimeCheckThree == false)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && myHaveEntered == true)
        {
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myFirstTimeCheckThree = false;
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision aCollision)
    {
        if (aCollision.gameObject.CompareTag("Player"))
        {
            myHaveEntered = true;
            Time.timeScale = 0.1f;
            myThirdTutorialText.SetActive(true);//skapa något, point to world? Fråga Sparky
        }
    }
}
