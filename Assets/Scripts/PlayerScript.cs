using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed;
    public float jumpCeiling, floorHeight, hurtTime;
    public GameObject PlayerHasBucket, PlayerNoBucket;

    bool onGround, isFalling, isJumping;
    bool hasBucket, isHurt;
    private void Update()
    {
        if (!GameManager.GAME.IsGamePaused())
        {
            //Get axis Input
            if (transform.position.x > -5.5f && Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            if (transform.position.x < -5.5f) transform.position = new Vector2(-5.5f, transform.position.y);

            if (transform.position.x < 24f && Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            if (transform.position.x > 24f) transform.position = new Vector2(24f, transform.position.y);



            //falling/Jumping
            if (transform.position.y < floorHeight + .01f)
            {
                transform.position = new Vector2(transform.position.x, floorHeight);
                onGround = true;
                isFalling = false;
                isJumping = false;
            }
            if (isFalling)
            {
                float _falldistance = moveSpeed * 2 * Time.deltaTime;
                if (transform.position.y * _falldistance > (floorHeight + .01f))
                {
                    transform.Translate(Vector2.down * _falldistance);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, floorHeight);
                    onGround = true;
                    isFalling = false;
                    isJumping = false;
                }

            }
            if (isJumping)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                if (transform.position.y > jumpCeiling)
                {
                    onGround = false;
                    isFalling = true;
                    isJumping = false;
                }
            }
            if (onGround && Input.GetButtonDown("Jump"))
            {
                if (!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.jump);
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                onGround = false;
                isFalling = false;
                isJumping = true;
            }
        }
    }

    public void HasBucketChange()
    {
        if (hasBucket)
        {
            PlayerNoBucket.SetActive(true);
            PlayerHasBucket.SetActive(false);
            GameManager.GAME.Pickup_Bucket.SetActive(true);
            PlayerNoBucket.GetComponent<Animator>().SetBool("Hurt", PlayerHasBucket.GetComponent<Animator>().GetBool("Hurt"));
            isHurt = PlayerNoBucket.GetComponent<Animator>().GetBool("Hurt");
        }
        if (!hasBucket)
        {
            PlayerNoBucket.SetActive(false);
            PlayerHasBucket.SetActive(true);
            PlayerHasBucket.GetComponent<Animator>().SetBool("Hurt", PlayerNoBucket.GetComponent<Animator>().GetBool("Hurt"));
            PlayerHasBucket.transform.GetChild(0).GetComponent<Animator>().SetBool("Full", false);
            isHurt = PlayerHasBucket.GetComponent<Animator>().GetBool("Hurt");
        }
        hasBucket = !hasBucket;        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feces" || collision.tag == "Urine")
        {
            if (!isHurt)
            {
                Destroy(collision.gameObject);
                GameManager.GAME.Health -= 10;
                transform.GetComponentInChildren<Animator>().SetBool("Hurt", true);
                isHurt = true;
                StartCoroutine(StopHurtTimer());
                if (GameManager.GAME.Health <= 0)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
                }
                if(!SoundManager.SFX.isPlaying) SoundManager.SFX.PlayOneShot(SoundManager.SOUND.hurt);
                Camera.main.GetComponent<Camera_Follows_Player>().ScreenShake(0.05f);
            }
        }
    }
    IEnumerator StopHurtTimer()
    {
        yield return new WaitForSeconds(hurtTime);
        transform.GetComponentInChildren<Animator>().SetBool("Hurt", false);
        isHurt = false;
    }
}
