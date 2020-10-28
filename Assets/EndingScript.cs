using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    [SerializeField] private GameObject myEndingCanvas = null;
    [SerializeField] private GameObject myCogsCollected = null;
    [SerializeField] private GameObject myLockedScene = null;
    [SerializeField] private GameObject myFirstScene = null;
    [SerializeField] private GameObject mySecondScene = null;
    [SerializeField] private GameObject myThirdScene = null;
    
    private Player myPlayer = null;
    //snakca med gustav om antal cogs

    //snacka med Leo om end gam how do

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Player>();

        if (myEndingCanvas == null)
        {
            Debug.LogError("myBG is fuckywucky");
        }
        if (myCogsCollected == null)
        {
            Debug.LogError("myCogsCollected is fuckywucky");
        }
        if (myLockedScene == null)
        {
            Debug.LogError("myLockedScreen is fuckywucky");
        }
        if (myFirstScene == null)
        {
            Debug.LogError("myFirstScene is fuckywucky");
        }
        if (mySecondScene == null)
        {
            Debug.LogError("mySecondScene is fuckywucky");
        }
        if (myThirdScene == null)
        {
            Debug.LogError("myThirdScene is fuckywucky");
        }

    }

    void Update()
    {
        
    }
    /*private void OnCollisionEnter(Collision anEndingCollider)
    {
        
    GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myLevelComplete = true;
        Player player = anEndingCollider.transform.GetComponentInParent<Player>();
        player.LevelComplete();
    }*/
    private void OnTriggerEnter(Collider anEndingCollider)
    {
        myPlayer.SetHasFinishedGame(true);
        Debug.Log("We out here fam");
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.myLevelComplete = true;
        Player player = anEndingCollider.transform.GetComponentInParent<Player>();
        player.LevelComplete();
        myEndingCanvas.SetActive(true);
        int cogsToDisplay = gameManager.TotalCogsCollected();
        myCogsCollected.GetComponent<Text>().text = cogsToDisplay + "/299"; //dubbelkolla on build

        if (cogsToDisplay >= 2)
        {
            mySecondScene.SetActive(true);
            if (cogsToDisplay >= 100)
            {
                myThirdScene.SetActive(true);
            }
        }
    }

    public void BackToMenu()
    {
        myPlayer.SetHasFinishedGame(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
