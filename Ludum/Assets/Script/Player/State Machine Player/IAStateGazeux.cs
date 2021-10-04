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
    private LineRenderer _lineRenderer;
    private Vector2 saveDirection;
    public int laserRange;
    public AudioClip[] sound;
    private AudioSource audioSource;
    private int randomSound;

    public override void StateStart()
    {
        audioSource = GetComponent<AudioSource>();
        _stateMachine.player.transform.GetComponent<BoxCollider2D>().size /= 2;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.useWorldSpace = true;
        randomSound = Random.Range(0, sound.Length);
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
        if (_stateMachine.playerController.rb2D.velocity.x == 0)
        {
            speedHz = 0;
        }
        if(_stateMachine.playerController.rb2D.velocity.y == 0)
        {
            speedVrt = 0;
        }

        _stateMachine.playerController.rb2D.velocity = new Vector2(hz + speedHz, vrt + speedVrt) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.clip = null;
            audioSource.clip = sound[randomSound];
            audioSource.Play();
        }


        int layerMask=3;
        if (Input.GetKey(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(_stateMachine.player.transform.position, saveDirection, laserRange, layerMask);
            Vector3 laserHit;
            laserHit = hit.point;
            //Debug.DrawLine(_stateMachine.player.transform.position, hit.point);
            _lineRenderer.SetPosition(0, _stateMachine.player.transform.position + new Vector3(saveDirection.x*2.5f, saveDirection.y*2.5f, 0));
            if (hit.collider)
            {
                _lineRenderer.SetPosition(1, new Vector3(laserHit.x, laserHit.y,0));
            }
            else
            {
                _lineRenderer.SetPosition(1, new Vector3(_stateMachine.player.transform.position.x, _stateMachine.player.transform.position.y+saveDirection.y*(laserRange/2), 0));
            }
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _lineRenderer.enabled = false;
            audioSource.Stop();
            randomSound = Random.Range(0, sound.Length);
        }
    }

    public override void Shoot(Vector2 mouseDirection)
    {
        saveDirection = new Vector2(mouseDirection.normalized.x * 2, mouseDirection.normalized.y * 2);
    }

    public override void StateEnd()
    {
        _lineRenderer.enabled = false;
        _stateMachine.player.transform.GetComponent<BoxCollider2D>().size *= 2;
        audioSource.Stop();
    }

    protected override string BuildGameObjectName()
    {
        return "GAZEUX";
    }

}
