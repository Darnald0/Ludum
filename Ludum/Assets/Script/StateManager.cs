using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
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
           stabilityNeutre = true;
           stabilityGaseous = false;
           stabilitySolide = false;
        }

        // il rentre dans le Statu Gazeux
        if (CurrentState >= MaxStability / 4)
        {
            Debug.Log("Gazeux state");
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
            Debug.Log("solide state");
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

        if (Input.GetKeyDown(KeyCode.Q))// lazzer fire 0.5 par 1 == 10
        {
            CurrentState -= fireState; // soit prendre la valeur du tir enemie ou une valeur de base 
        }

        if (Input.GetKeyDown(KeyCode.E))// bullet fire 1 == 5
        {
            CurrentState += fireState; // probleme laser nick cette mechanic
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
        Debug.Log("EXPLOSION!!!!! putaiinnn ...");
        UnstableReady = false;
        //StartCoroutine(Wait(waitAnim));
        //Player.Life --;
        CurrentState = 0;


    }

    //IEnumerator Wait(int waitAnim)
    //{
    //    Nova.SetBool("Open", true);
    //    yield return new WaitForSeconds(waitAnim);
    //    Nova.SetBool("Open", false);
    //}
}



