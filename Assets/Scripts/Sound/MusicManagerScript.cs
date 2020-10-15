using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*******************************************
* Test-funktioner just nu för kontinuerlig bakgrundsmusik.
* Just nu endast en fil (menymusik).
* 
* PlayMenuMusic();
* StopMenuMusic();
* 
* Musiken måste stängas av i slutet av varje bana/åk/död.
* Tanken är att utveckla vidare till separat musik 
* för Meny samt respektive bana. 
* 
* 
*******************************************/

public class MusicManagerScript : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip myBackGroundNoiseClip;

    [SerializeField]
    private AudioClip myMenuMusicClip;

    //[SerializeField]
    //private AudioClip myScene01MusicClip;
    //[SerializeField]
    //private AudioClip myScene02MusicClip;
    //[SerializeField]
    //private AudioClip myScene03MusicClip;
    //[SerializeField]
    //private AudioClip myScene04MusicClip;



    //[SerializeField]
    //private AudioClip myPauseResumeClip;
    //[SerializeField]
    //private AudioClip myQuitGameClip;

    //[SerializeField]
    //private AudioClip myBoardHumClip;
    //[SerializeField]
    //private AudioClip myBoardJumpClip;
    //[SerializeField]
    //private AudioClip myBoardLandClip;

    [SerializeField]
    private AudioClip myCoinCollectClip;
    //[SerializeField]
    //private AudioClip myRailRideClip;
    //[SerializeField]
    //private AudioClip myPowerUpClip;
    //[SerializeField]
    //private AudioClip myPowerDownClip;
    //[SerializeField]
    //private AudioClip myCollisionClip;
    //[SerializeField]
    //private AudioClip myDeathCrashClip;

    //[SerializeField]
    private AudioSource myMusicSource;
    ////[SerializeField]
    //private AudioSource mySoundFXSource;
    ////[SerializeField]
    //private AudioSource myBackgroundNoiseSource;
    ////[SerializeField]
    //private AudioSource myBoardSoundSource;


    //[SerializeField]
    private float myMusicVolume = 1f; 
    [SerializeField]
    private float myFadeTime = 0.5f;  //sek, ca

    private bool myFadeUp = false;
    private bool myFadeDown = false;

    void Start()
    {
        myMusicSource = GetComponent<AudioSource>();
        if (myMusicSource == null)
        {
            Debug.LogError("Music Audio source is NULL. ");
        }
        else
        {
            myMusicVolume = 1f;
            myMusicSource.clip = myMenuMusicClip;
        }

        //mySoundFXSource = GetComponent<AudioSource>();
        //if (mySoundFXSource == null)
        //{
        //    Debug.LogError("FX Audio source is NULL. ");
        //}


    }

    void Update()
    {
        // bara för test!
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayMenuMusic();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopMenuMusic();
        }

        if (myFadeUp)
        {
            myMusicVolume += Time.deltaTime / myFadeTime;
            if (myMusicVolume > 1f)
            {
                myMusicVolume = 1;
                myFadeUp = false;
            }
            myMusicSource.volume = myMusicVolume;
        }

        if (myFadeDown)
        {
            myMusicVolume -= Time.deltaTime / myFadeTime;
            if (myMusicVolume <= 0.01f)
            {
                myMusicVolume = 0;
                myMusicSource.Stop();
                myFadeDown = false;
            }
            myMusicSource.volume = myMusicVolume;
        }
    }


    public void PlayMenuMusic ()
    {
        myMusicSource.Play();
        myFadeUp = true;
    }
    public void StopMenuMusic ()
    {
        myFadeDown = true;
    }

    void PlayCoinSound()
    {

    }

}

