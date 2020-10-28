using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManagerScript : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip myBackGroundNoiseClip;

    [SerializeField]
    private AudioClip myMenuMusicClip00;
    //[SerializeField]
    //private AudioClip myMenuMusicClip01;
    [SerializeField]
    private AudioClip myScene03WinMusic;


    //[SerializeField]
    //private AudioClip myCreditsClip;

    [SerializeField]
    private AudioClip myScene01MusicClip;
    [SerializeField]
    private AudioClip myScene02MusicClip;
    [SerializeField]
    private AudioClip myScene03MusicClip;




    //[SerializeField]
    private AudioSource myMusicSource;


    //[SerializeField]
    private float myMusicVolume = 1f; 
    //[SerializeField]
    private float myFadeTime = 0.4f;  //sek, ca

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
            myMusicSource.clip = myMenuMusicClip00;
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
        if (Input.GetKeyDown(KeyCode.X))  //  Man behöver ingen separat playfunktion nedan funkar. Bra om mna kan fada ut dock!  2020 10 20 BE
        {
            PlayMusic();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopMusic();
        }

        // bara för test!
        if (Input.GetKeyDown(KeyCode.A))        //FUNKAR!! 2020 10 20 BE
        {
            StopMusic();
            myMusicSource.clip = myScene01MusicClip;
            PlayMusic();
        }
        if (Input.GetKeyDown(KeyCode.S))        //FUNKAR!! 2020 10 20 BE
        {
            StopMusic();
            myMusicSource.clip = myMenuMusicClip00;
            PlayMusic();
        }


        //if (myFadeUp)
        //{
        //    myMusicVolume += Time.deltaTime / myFadeTime;
        //    if (myMusicVolume > 1f)
        //    {
        //        myMusicVolume = 1;
        //        myFadeUp = false;
        //    }
        //    myMusicSource.volume = myMusicVolume;
        //}

        //if (myFadeDown)
        //{
        //    myMusicVolume -= Time.deltaTime / myFadeTime;
        //    if (myMusicVolume <= 0.01f)
        //    {
        //        myMusicVolume = 0;
        //        myMusicSource.Stop();
        //        myFadeDown = false;
        //    }
        //    myMusicSource.volume = myMusicVolume;
        //}
    }


    public void PlayMusic ()
    {
        StopMusic();
        myMusicSource.clip = myMenuMusicClip00;
        myMusicSource.Play();  //!
        //myFadeUp = true; //!
    }    
    public void PlayMenuMusic_00 ()
    {
        StopMusic();
        myMusicSource.clip = myMenuMusicClip00;
        myMusicSource.Play();  //!
        //myFadeUp = true; //!
    }    
    public void PlayMenuMusic_01 ()
    {
        StopMusic();
        myMusicSource.clip = myMenuMusicClip00;
        myMusicSource.Play();  //!
        //myFadeUp = true; //!
    }
    public void PlayScene03WinningMusic()
    {
        StopMusic();
        //myMusicSource.clip = myScene03WinMusic;
        myMusicSource.PlayOneShot(myScene03WinMusic);  //! !!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //myFadeUp = true; //!
    }



    public void PlayMusic01 ()
    {
        StopMusic();
        myMusicSource.clip = myScene01MusicClip;
        myMusicSource.Play();  //!
        //myFadeUp = true; //!
    }    
    public void PlayMusic02 ()
    {
        StopMusic();
        myMusicSource.clip = myScene02MusicClip;
        myMusicSource.Play();  //!
        //myFadeUp = true; //!
    }    
    public void PlayMusic03 ()
    {
        StopMusic();
        myMusicSource.clip = myScene03MusicClip;
        myMusicSource.Play();  //!
        //myFadeUp = true; //!
    }


    public void StopMusic ()
    {
        myMusicSource.Stop();  //EXTRA
        //myFadeDown = true;  //!
    }

    void PlayCoinSound()
    {

    }

}

