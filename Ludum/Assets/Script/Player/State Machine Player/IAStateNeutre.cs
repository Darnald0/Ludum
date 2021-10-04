using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IAStateNeutre : AIAState
{
    public int speed;
    public GameObject bullet;
    private bool canShoot = true;
    public AudioClip[] sound;
    private AudioSource audioSource;

    public override void StateStart()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void StateUpdate()
    {
        float hz = Input.GetAxis("Horizontal");
        float vrt = Input.GetAxis("Vertical");
        _stateMachine.playerController.rb2D.velocity = new Vector2(hz * speed, vrt * speed) * Time.deltaTime;
    }

    public override void Shoot(Vector2 mouseDirection)
    {
        if (canShoot)
        {
            int randomSound = Random.Range(0, sound.Length);
            audioSource.PlayOneShot(sound[randomSound]);
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject save = Instantiate(bullet, transform.position + new Vector3(mouseDirection.normalized.x * 10, mouseDirection.normalized.y * 10, 0), Quaternion.Slerp(transform.rotation, rotation, 1));
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
