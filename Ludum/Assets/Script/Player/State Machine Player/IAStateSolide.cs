using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStateSolide : AIAState
{
    public int speed;
    public float forceThrow;
    public float gravityToApply;
    private bool shot;
    public bool canShoot = true;
    private Vector2 saveDirection;
    public GameObject arrow;
    public PhysicsMaterial2D bounceMaterial;
    [HideInInspector]
    public bool touchWall;
    public AudioClip[] sound;
    private AudioSource audioSource;

    public override void StateStart()
    {
        audioSource = GetComponent<AudioSource>();
        _stateMachine.player.transform.GetComponent<BoxCollider2D>().size *= 2;

        _stateMachine.playerController.rb2D.gravityScale = gravityToApply;
        arrow.SetActive(true);
        _stateMachine.playerController.rb2D.angularDrag = 0;
        _stateMachine.playerController.rb2D.sharedMaterial = bounceMaterial;
    }

    public override void StateUpdate()
    {
        float hz = Input.GetAxis("Horizontal");
        _stateMachine.playerController.rb2D.velocity = new Vector2(hz * speed, 0) * Time.deltaTime;

        if (shot)
        {
            arrow.SetActive(false);
            if (!touchWall || _stateMachine.playerController.rb2D.velocity == new Vector2(0,0))
            {
                _stateMachine.playerController.rb2D.AddForce(saveDirection*50* forceThrow);
            }
        }
    }

    public override void Shoot(Vector2 mouseDirection)
    {
        if (!shot && canShoot)
        {
            int randomSound = Random.Range(0, sound.Length);
            audioSource.PlayOneShot(sound[randomSound]);
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
        touchWall = false;
    }

    public override void StateEnd()
    {
        _stateMachine.player.transform.GetComponent<BoxCollider2D>().size /= 2;

        _stateMachine.playerController.rb2D.gravityScale = 0;
        StopAllCoroutines();
        canShoot = true;
        shot = false;
        arrow.SetActive(false);

        _stateMachine.playerController.rb2D.angularDrag = 0.05f;
        _stateMachine.playerController.rb2D.sharedMaterial = null;
        audioSource.Stop();
        _stateMachine.playerController.anim.enabled = false;
    }

    protected override string BuildGameObjectName()
    {
        return "SOLIDE";
    }
}