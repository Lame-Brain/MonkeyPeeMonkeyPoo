using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GAME;
    public GameObject Pickup_Bucket, DropOff_Bucket, Player, MonkeyRoot, UI, MessagePrefab, Tutorial;
    public GameObject[] Monkeys;
    public TMPro.TextMeshProUGUI SamplesCollected_GUI, Health_GUI, BucketNumSamples_GUI, Timer_GUI;
    public int Points, Health, BucketSamples;
    public float TimeLeft;

    bool _paused;
    GameObject _go;
    GameObject _message;
    float Message_Length = 1f;

    private void Awake()
    {
        GAME = this;
        DontDestroyOnLoad(GAME);
        _message = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _paused = false;
        if (SoundManager.SOUND.First_Time_Through)
        {
            Tutorial.SetActive(true);
            SoundManager.SOUND.First_Time_Through = false;
            _paused = true;
        }        
        StartCoroutine(TimerCountsDown());
        for (int _i = 0; _i < (Monkeys.Length * 3); _i++)
        {
            _go = Instantiate(Monkeys[Random.Range(0, Monkeys.Length)], new Vector2(Random.Range(-5, 23), Random.Range(MonkeyRoot.transform.position.y - .5f, MonkeyRoot.transform.position.x + .5f)), Quaternion.identity, MonkeyRoot.transform);
            _go.GetComponent<MonkeyScript>().moveSpeed = Random.Range(1f, 2.5f);
            _go.GetComponent<MonkeyScript>().waveHeight = Random.Range(.5f, 1.5f);
            _go.GetComponent<MonkeyScript>().fireDelay = Random.Range(2f, 7f);
            _go.GetComponent<MonkeyScript>().poopChance = Random.Range(15f, 50f);
            if (Random.Range(1, 11) < 6) _go.transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void Update()
    {
        //Update GUI each frame
        SamplesCollected_GUI.text = "Samples Collected: " + Points;
        Health_GUI.text = "Health: " + Health;
        BucketNumSamples_GUI.text = "Samples in Bucket: " + BucketSamples;
        Timer_GUI.text = "Time Left: " + TimeLeft.ToString("00.00");
    }

    IEnumerator TimerCountsDown()
    {
        yield return new WaitForSeconds(.05f);
        if (!_paused)
        {
            TimeLeft -= .01f;
            if (TimeLeft <= 0) UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
            StartCoroutine(TimerCountsDown());
        }
    }

    public void PauseGame()
    {
        if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.beep);
        _paused = !_paused;
        if (_paused)
        {
            Tutorial.SetActive(true);
            //StartCoroutine(TimerCountsDown());
        }
        if (!_paused)
        {
            Tutorial.SetActive(false);
            StartCoroutine(TimerCountsDown());
        }
    }

    public bool IsGamePaused() { return _paused; }

    public void PopMessage(string text)
    {
        _message = Instantiate(MessagePrefab, UI.transform);
        _message.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        StartCoroutine(KillMessage(_message));
    }
    IEnumerator KillMessage(GameObject message)
    {
        yield return new WaitForSeconds(Message_Length);
        if (message != null) Destroy(message);
    }

    public void QuitGame() { UnityEngine.SceneManagement.SceneManager.LoadScene("IntroScreen"); }
}