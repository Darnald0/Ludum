using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public float pointsPerSecond = 2;
    public TMP_Text scoreText;

    private double score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        score += pointsPerSecond * Time.deltaTime;
        scoreText.text = ((long)score).ToString();
    }

    public int getScore()
    {
        return (int)score;
    }
    public void addScore(float value)
    {
        score += value;
    }
    public void removeScore(float value)
    {
        score -= value;
    }
}
