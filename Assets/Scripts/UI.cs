using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
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



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Tryck ESC - Pause()
    }


    void Pause()
    {
        Time.timeScale = 0; //TEMPORÄR LÖSNING!!!! 
                            //pga kan bli kukigt om man inte kan röra sig i pausmenyn.
        
        //Get canvas for pause

        //Knappfunktioner för hur canvas fung - kodat eller skött in engine?
    }

}
