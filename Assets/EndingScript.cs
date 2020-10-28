using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    [SerializeField] private GameObject myBG = null;
    [SerializeField] private GameObject myCogsCollected = null;
    [SerializeField] private GameObject myLockedScene = null;
    [SerializeField] private GameObject myFirstScene = null;
    [SerializeField] private GameObject mySecondScene = null;
    [SerializeField] private GameObject myThirdScene = null;

    //snakca med gustav om antal cogs

    //snacka med Leo om end gam how do

    void Start()
    {
        if (myBG == null)
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
        /*if mycogscollected >=50
          SetActive mySecondScreen*/

        /*if mycogscollected >=100
          SetActive myThirdScreen*/

    }
    /*private void OnCollisionEnter(Collision anEndingCollider)
    {
        
    GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myLevelComplete = true;
        Player player = anEndingCollider.transform.GetComponentInParent<Player>();
        player.LevelComplete();
    }*/
    private void OnTriggerEnter(Collider anEndingCollider)
    {

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().myLevelComplete = true;
        Player player = anEndingCollider.transform.GetComponentInParent<Player>();
        player.LevelComplete();
    }
}
