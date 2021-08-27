using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ReasonText, ScoreScreen;
    public GameObject ReadyText;
    private bool _ready;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GAME.TimeLeft <= 0)
        {
            ReasonText.text = "OUT OF TIME!";
        }
        else
        {
            ReasonText.text = "OUT OF HEALTH!";
        }
        ScoreScreen.text = "You Collected " + GameManager.GAME.Points + " Samples";
        Destroy(GameManager.GAME.gameObject);
        _ready = false;
        StartCoroutine(WaitForReady());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _ready)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("IntroScreen");
        }
    }

    IEnumerator WaitForReady()
    {
        yield return new WaitForSeconds(1f);
        ReadyText.SetActive(true);
        _ready = true;
    }
}
