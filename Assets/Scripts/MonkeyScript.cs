using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyScript : MonoBehaviour
{
    public float moveSpeed;
    public float waveHeight;
    public float fireDelay;
    public float poopChance;
    public GameObject fecesPrefab, urinePrefab;

    private Vector3 _newPos;
    private GameObject _go;


    private void Start()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//could replace 0 by any other animation layer index
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        InvokeRepeating("Shoot", fireDelay, fireDelay);
    }

    void Update()
    {
        _newPos = transform.position;
        _newPos.y += Mathf.Sin(Time.time) * waveHeight * Time.deltaTime;
        transform.position = _newPos;
        Move();
    }

    void Move()
    {
        transform.Translate((Vector2.left * moveSpeed) * Time.deltaTime);

        if (transform.position.x < -5.35f) transform.eulerAngles = new Vector2(0, 180);
        if (transform.position.x > 23.90f) transform.eulerAngles = new Vector2(0, 0);
    }

    void Shoot()
    {
        if (Random.Range(0f, 100f) > poopChance)
        {
            _go = Instantiate(fecesPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            _go = Instantiate(urinePrefab, transform.position, Quaternion.identity);
        }
        _go.GetComponent<Projectile>().moveSpeed = Random.Range(2f, 5f);
    }
}
