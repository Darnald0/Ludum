using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave", order = 1)]
public class WaveObject : ScriptableObject
{
    public GameObject[] slots = new GameObject[5];
    public int nextWave = 0;
}
