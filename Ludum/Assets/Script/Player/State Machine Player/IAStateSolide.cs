using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStateSolide : AIAState
{
    public int speed;
    private int realSpeed;
    public float forceThrow;
    private bool shot;
    private bool canShoot = true;
    private Vector2 saveDirection;
    public GameObject arrow;

    public override void StateStart()
    {
        _stateMachine.player.transform.localScale *= 2;
        realSpeed = speed;
        arrow.SetActive(true);
    }

    public override void StateUpdate()
    {
        float hz = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            realSpeed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            realSpeed /= 2;
        }
        _stateMachine.playerController.rb2D.velocity = new Vector2(hz * realSpeed, 0) * Time.deltaTime;

        if (shot)
        {
            arrow.SetActive(false);
            _stateMachine.playerController.rb2D.AddForce(saveDirection * forceThrow);
        }
    }

    public override void Shoot(Vector2 mouseDirection)
    {
        if (!shot && canShoot)
        {
            saveDirection = mouseDirection;
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        shot = true;
        canShoot = false;
        yield return new WaitForSeconds(0.2f);
        shot = false;
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
        arrow.SetActive(true);
    }

    public override void StateEnd()
    {
        _stateMachine.player.transform.localScale /= 2;
        StopAllCoroutines();
        canShoot = true;
        shot = false;
        arrow.SetActive(false);
    }

    protected override string BuildGameObjectName()
    {
        return "SOLIDE";
    }
}