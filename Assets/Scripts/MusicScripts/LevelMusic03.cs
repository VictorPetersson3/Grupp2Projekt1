using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic03 : MonoBehaviour
{
    private MusicManagerScript myMusicManager;   //Elf

    // Start is called before the first frame update
    void Start()
    {
        myMusicManager = GameObject.FindGameObjectsWithTag("MusicManager")[0].GetComponent<MusicManagerScript>();  //Elf
        myMusicManager.PlayMusic03();   //Elf
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
