using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame_Cinematic : MonoBehaviour
{
    [SerializeField]
    private Animator myShipAnimator;
    
    [SerializeField]
    private GameObject myShip;

    [SerializeField]
    private Player myPlayer;


    void Update()
    {
        if(myPlayer.GetHasSeenCutscene())
        {
            Destroy(myShip);
            this.gameObject.SetActive(false);
        }
        else
        {
            if (myShipAnimator.GetCurrentAnimatorStateInfo(0).IsName("MothershipAnimation") && myShipAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(myShip);
                this.gameObject.SetActive(false);
            }
        }
    }
}
