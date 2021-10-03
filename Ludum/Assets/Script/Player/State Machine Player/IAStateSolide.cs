using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStateSolide : AIAState
{
    public int speed;
    private int realSpeed;

    public override void StateStart()
    {
        _stateMachine.player.transform.localScale *= 2;
        realSpeed = speed;
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
    }

    public override void StateEnd()
    {
        _stateMachine.player.transform.localScale /= 2;
    }

    protected override string BuildGameObjectName()
    {
        return "SOLIDE";
    }
}