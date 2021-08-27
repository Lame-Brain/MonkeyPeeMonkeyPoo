using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreenScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
