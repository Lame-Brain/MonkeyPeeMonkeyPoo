using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_Controller : MonoBehaviour
{
    string _mode;

    void Start()
    {
        _mode = this.gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_mode == "Pickup_Bucket")
            {
                GameManager.GAME.Pickup_Bucket.SetActive(false);
                GameManager.GAME.Player.GetComponent<PlayerScript>().HasBucketChange();
                if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.grab);
            }
            if (_mode == "Dropoff_Location")
            {
                if (GameManager.GAME.Player.GetComponent<PlayerScript>().PlayerHasBucket.activeSelf && GameManager.GAME.BucketSamples > 0 && !GameManager.GAME.DropOff_Bucket.activeSelf)
                {
                    GameManager.GAME.PopMessage(GameManager.GAME.BucketSamples + " SAMPLES DELIVERED!");
                    GameManager.GAME.Player.GetComponent<PlayerScript>().HasBucketChange();
                    GameManager.GAME.Points += GameManager.GAME.BucketSamples;
                    GameManager.GAME.BucketSamples = 0;
                    GameManager.GAME.DropOff_Bucket.SetActive(true);
                    StartCoroutine(CountUpSamples());
                    if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.grab);
                }
            }
        }
        if (_mode == "Collection_Bucket")
        {
            if (collision.tag == "Urine")
            {
                if (GameManager.GAME.BucketSamples < 10) GameManager.GAME.BucketSamples++;
                if (GameManager.GAME.BucketSamples > 5) transform.GetComponent<Animator>().SetBool("Full", true);
                if (GameManager.GAME.BucketSamples == 10) GameManager.GAME.PopMessage("Bucket Full");
                if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.get);
                Destroy(collision.gameObject);
            }
            if (collision.tag == "Feces")
            {
                GameManager.GAME.BucketSamples = 0;
                GameManager.GAME.Player.GetComponent<PlayerScript>().HasBucketChange();
                Destroy(collision.gameObject);
                GameManager.GAME.PopMessage("BUCKET CONTAMINATED!");
                if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.badGet);
            }
        }
    }
    IEnumerator CountUpSamples()
    {
        yield return new WaitForSeconds(1f);
        GameManager.GAME.DropOff_Bucket.SetActive(false);
    }
}
