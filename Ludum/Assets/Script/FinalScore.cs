using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_Text>().text = "" + ScoreManager.instance.getScore();
    }
}
