using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    public int damage;
    [HideInInspector] public bool isPlayerBullet = false;
    [HideInInspector] public bool isRedInk;
    private SpriteRenderer sr;
    private void Awake()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (isRedInk)
            sr.color = Color.red;
        StartCoroutine(TimeToDie());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right*speed* Time.deltaTime;
    }

    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerBullet)
        {
            if(collision.tag != "Player")
            {
                if (collision.tag == "Enemy")
                {
                    //    var score = collision.GetComponent<EnemyManager>().scoreValue;
                    //    ScoreManager.instance.addScore(score);
                    collision.GetComponent<EnemyManager>().hp -= damage;
                }
                Destroy(this.gameObject);
            }

        }

        if (!isPlayerBullet)
        {
            if(collision.tag == "Player")
            {
                if (isRedInk)
                {
                    StateManager.instance.GetHit(damage, EnemyManager.Type.neutral);
                    Destroy(this.gameObject);
                } else
                {
                    StateManager.instance.GetHit(-damage, EnemyManager.Type.neutral);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
