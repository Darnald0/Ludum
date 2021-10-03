using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IAStateNeutre : AIAState
{
    public int speed;
    private int realSpeed;

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
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            realSpeed /= 2;
        }

        _stateMachine.playerController.rb2D.velocity = new Vector2(hz * realSpeed, vrt * realSpeed) * Time.deltaTime;
    }

    protected override string BuildGameObjectName()
    {
        return "NEUTRE";
    }
}
