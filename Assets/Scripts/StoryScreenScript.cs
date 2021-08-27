using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScreenScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.beep);            
            UnityEngine.SceneManagement.SceneManager.LoadScene("IntroScreen");            
        }
    }
}
