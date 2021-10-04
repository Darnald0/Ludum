using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public PlayerController playerController;

    public int MaxStability;
    public int StabilityTimer = 5;

    public Slider sliderUnstable;

    //public GameObject nova;
    //public Animator Nova;
    private float CurrentState;
    private bool UnstableReady = false;

    public int fireState = 5;
    [HideInInspector]
    public int waitAnim = 2;

    [HideInInspector]
    public bool stabilityNeutre = true;
    [HideInInspector]
    public bool stabilityGaseous = false;
    [HideInInspector]
    public bool stabilitySolide = false;

    public Image Tete;

    public Sprite TeteNeutre;
    public Sprite TeteGazeux;
    public Sprite TeteSolide;

    public void Start()
    {
        CurrentState = 0f;
        sliderUnstable.maxValue = MaxStability;
        sliderUnstable.minValue = -MaxStability;
    }

    public void Update()
    {
        SetUnstable(CurrentState);

        if(CurrentState < MaxStability / 4 || CurrentState < -MaxStability / 4)
        {
            Tete.sprite = TeteNeutre;
            stabilityNeutre = true;
            stabilityGaseous = false;
            stabilitySolide = false;
        }

        // il rentre dans le Statu Gazeux
        if (CurrentState >= MaxStability / 4)
        {
            Tete.sprite = TeteGazeux;
            stabilityNeutre = false;
            stabilityGaseous = true;
            stabilitySolide = false;
            CurrentState += StabilityTimer/2 * Time.deltaTime;
        }

        if (CurrentState >= MaxStability / 2)
        {
            CurrentState += StabilityTimer * Time.deltaTime;
        }

        // il rentre dans le Statu solide
        if (CurrentState <= -MaxStability / 4)
        {
            Tete.sprite = TeteSolide;
            stabilityNeutre = false;
            stabilityGaseous = false;
            stabilitySolide = true;
            CurrentState -= StabilityTimer/2 * Time.deltaTime;
        }

        if (CurrentState <= -MaxStability / 2)
        {
            CurrentState -= StabilityTimer * Time.deltaTime;
        }

        if (CurrentState >= MaxStability || CurrentState <= -MaxStability)
        {
            UnstableReady = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CurrentState -= fireState;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CurrentState += fireState;
        }

        if (UnstableReady)
        {
            UnstableAnim();
        }
    }

    public void SetUnstable(float state)
    {
        sliderUnstable.value = state;
    }
    public void UnstableAnim()
    {
        UnstableReady = false;
        playerController.life --;
        CurrentState = 0;
    }

    //IEnumerator Wait(int waitAnim)
    //{
    //    Nova.SetBool("Open", true);
    //    yield return new WaitForSeconds(waitAnim);
    //    Nova.SetBool("Open", false);
    //}
}



