using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceScript : MonoBehaviour
{
    public GameObject myScoreText;
    public static int myCogsCollected;
    public static int myTotalCogsToCollect;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        myScoreText.GetComponent<Text>().text = myCogsCollected + "/" + myTotalCogsToCollect;
    }
    
    
    /*Skriver pseudokod så länge,
     * i väntan på att jag vet vilka
     * element som bör användas osv*/

        /*Huvudmenygrejer (eget script?)
         * 
         * 
         * mtp att knappar = element in game,
         * funkt i kod? el. in engine?
         * i fallet play game, beror på om 
         * meny är scene eller canvas
         * 
         * 
         * 
         */


        /*HUD-grejer
         * 
         * srsly, eget script? mest in engine?
         * 
         * Rotation för kugghjul, blechhhh
         * siffror, sköts ju av kollision
         * 
         */



    

    void Pause()
    {
        Time.timeScale = 0; //TEMPORÄR LÖSNING!!!! 
                            //pga kan bli kukigt om man inte kan röra sig i pausmenyn.
        
        //Get canvas for pause

        //Knappfunktioner för hur canvas fung - kodat eller skött in engine?
    }

}
