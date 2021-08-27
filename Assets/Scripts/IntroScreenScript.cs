using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreenScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.beep);
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}
