using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.GAME.IsGamePaused()) transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        if (transform.position.y < -4.5) Destroy(this.gameObject);
    }
}
