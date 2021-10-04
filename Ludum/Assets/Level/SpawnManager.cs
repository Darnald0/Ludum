using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawner = new GameObject[5];
    public List<WaveObject> waves;

    private int wavePlayed = 0;
    private bool onWait = false;


    void Update()
    {
        
        if(waves.Count > 0 && wavePlayed < waves.Count)
        {
            StartCoroutine(startSpawner());
        }
    }

    IEnumerator startSpawner()
    {
        for (int i = 0; i < waves.Count; i++)
        {

            if (!onWait)
            {

                wavePlayed++;
                SpawnWave(waves[i]);

                if (waves[i].nextWave > 0)
                {
                    onWait = true;
                    yield return new WaitForSeconds(waves[i].nextWave);
                    onWait = false;

                }
                else
                {
                    onWait = true;
                    yield return new WaitForSeconds(waves[i].nextWave);
                    onWait = false;
                }
            }
            else
            {
                Debug.Log("here");
                StartCoroutine(WaitUntil(!onWait));
            }
        }
    }

    void SpawnWave(WaveObject wave)
    {
        for(int j=0; j < wave.slots.Length; j++)
        {
            if(wave.slots[j])
            {
                GameObject obj = GameObject.Instantiate(wave.slots[j], spawner[j].transform);
            }
        }
    }

    IEnumerator Wait(float time)
    {
        Debug.Log("WAIT : " + onWait);
        yield return new WaitForSeconds(time);
        onWait = false;
    }

    IEnumerator WaitUntil(bool until)
    {
        Debug.Log("WAIT : " + onWait);
        yield return new WaitUntil(() => until);
        onWait = false;
    }
}
