using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SOUND;
    public AudioSource music, ambience, sfx;
    public static AudioSource MUSIC, AMBIENCE, SFX;
    public AudioClip badGet, beep, bucketNoise, get, grab, hurt, jump;


    public bool First_Time_Through; //THIS IS A HACK

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SOUND = this;
        SFX = sfx;
        AMBIENCE = ambience;
        MUSIC = music;
        First_Time_Through = true;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainScene") 
        {            
            if (Random.Range(0f, 100f) < 1f) if(!AMBIENCE.isPlaying) AMBIENCE.PlayOneShot(ambience.clip);
        }
    }
}
