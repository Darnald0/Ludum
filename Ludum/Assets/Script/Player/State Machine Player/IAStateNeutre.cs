using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IAStateNeutre : AIAState
{
    public int speed;
    private int realSpeed;

    public GameObject bullet;
    private bool canShoot = true;
    public override void StateStart()
    {
        realSpeed = speed;
    }

    public override void StateUpdate()
    {
        float hz = Input.GetAxis("Horizontal");
        float vrt = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            realSpeed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            realSpeed /= 2;
        }

        _stateMachine.playerController.rb2D.velocity = new Vector2(hz * realSpeed, vrt * realSpeed) * Time.deltaTime;
    }

    public override void Shoot(Vector2 mouseDirection)
    {
        if (canShoot)
        {
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject save = Instantiate(bullet, transform.position + new Vector3(mouseDirection.normalized.x * 6, mouseDirection.normalized.y * 6, 0), Quaternion.Slerp(transform.rotation, rotation, 1));
            save.GetComponent<Bullet>().isPlayerBullet = true;
            save.tag = "Player";
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }

    public override void StateEnd()
    {
        StopAllCoroutines();
        canShoot = true;
    }

    protected override string BuildGameObjectName()
    {
        return "NEUTRE";
    }
}
