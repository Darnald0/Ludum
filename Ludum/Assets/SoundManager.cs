using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public float delaySound;
    public AudioSource audioSource;

    private void Awake()
    {
        StartCoroutine(delay(delaySound));
    }

    IEnumerator delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
    }

}
