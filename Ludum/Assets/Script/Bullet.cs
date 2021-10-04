using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    public int damage;
    [HideInInspector] public bool isPlayerBullet = false;

    private void Start()
    {
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
                StateManager.instance.GetHit(damage, EnemyManager.Type.neutral);
                Destroy(this.gameObject);
            }
        }
    }
}
