using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IAStateGazeux : AIAState
{

    private float speedHz=0, speedVrt=0;
    public float maxSpeed;
    public float acceleration,deceleration;
    public float timeMultipicateur;

    public override void StateStart()
    {
        _stateMachine.player.transform.localScale /= 2;
    }

    public override void StateUpdate()
    {
        float hz = Input.GetAxis("Horizontal");
        float vrt = Input.GetAxis("Vertical");
        #region MovementHorizontal
        if (hz<0 && speedHz > -maxSpeed)
        {
            speedHz = speedHz - acceleration * Time.deltaTime* timeMultipicateur;
            if (speedHz < -maxSpeed)
            {
                speedHz = -maxSpeed;
            }
        }
        else if(hz>0 && speedHz<maxSpeed)
        {
            speedHz = speedHz + acceleration * Time.deltaTime* timeMultipicateur;
            if (speedHz > maxSpeed)
            {
                speedHz = maxSpeed;
            }
        }
        else if (hz == 0 && speedHz != 0)
        {
            if (speedHz > 0)
            {
                speedHz = speedHz - deceleration * Time.deltaTime* timeMultipicateur;
                if (speedHz < 0)
                {
                    speedHz = 0;
                }
            }
            else if (speedHz < 0)
            {
                speedHz = speedHz + deceleration * Time.deltaTime* timeMultipicateur;
                if (speedHz > 0)
                {
                    speedHz = 0;
                }
            }
        }
        #endregion
        #region MovementVertical
        if (vrt < 0 && speedVrt > -maxSpeed)
        {
            speedVrt = speedVrt - acceleration * Time.deltaTime* timeMultipicateur;
            if (speedVrt < -maxSpeed)
            {
                speedVrt = -maxSpeed;
            }
        }
        else if (vrt > 0 && speedVrt < maxSpeed)
        {
            speedVrt = speedVrt + acceleration * Time.deltaTime* timeMultipicateur;
            if (speedVrt > maxSpeed)
            {
                speedVrt = maxSpeed;
            }
        }
        else if(vrt==0 && speedVrt != 0)
        {
            if (speedVrt > 0)
            {
                speedVrt = speedVrt - deceleration * Time.deltaTime* timeMultipicateur;
                if (speedVrt < 0)
                {
                    speedVrt = 0;
                }
            }
            else if (speedVrt < 0)
            {
                speedVrt = speedVrt + deceleration * Time.deltaTime* timeMultipicateur;
                if (speedVrt > 0)
                {
                    speedVrt = 0;
                }
            }
        }
        #endregion

        Debug.Log("Hz : " + speedHz);
        Debug.Log("Vrt : " + speedVrt);

        _stateMachine.playerController.rb2D.velocity = new Vector2(hz + speedHz, vrt + speedVrt) * Time.deltaTime;
    }

    public override void StateEnd()
    {
        _stateMachine.player.transform.localScale *= 2;
    }

    protected override string BuildGameObjectName()
    {
        return "GAZEUX";
    }

}
