using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
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

    private AudioSource audioSource;

    public Sprite playerGaz;
    public Sprite playerGaz2;
    public Sprite playerNeutre;
    public Sprite playerSolid;
    public Sprite playerSolid2;

    public void Awake()
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
    public void Start()
    {
        CurrentState = 0f;
        sliderUnstable.maxValue = MaxStability;
        sliderUnstable.minValue = -MaxStability;
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        SetUnstable(CurrentState);

        if(CurrentState < MaxStability / 4 && CurrentState > -MaxStability / 4)
        {
            Tete.sprite = TeteNeutre;
            stabilityNeutre = true;
            stabilityGaseous = false;
            stabilitySolide = false;
            audioSource.Stop();
            playerController.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerNeutre;
        }

        // il rentre dans le Statu Gazeux
        if (CurrentState >= MaxStability / 2)
        {
            CurrentState += StabilityTimer * Time.deltaTime;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            playerController.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerGaz2;
        }
        else if (CurrentState >= MaxStability / 4)
        {
            Tete.sprite = TeteGazeux;
            stabilityNeutre = false;
            stabilityGaseous = true;
            stabilitySolide = false;
            CurrentState += StabilityTimer/2 * Time.deltaTime;
            audioSource.Stop();
            playerController.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerGaz;
        }

        // il rentre dans le Statu solide
        if (CurrentState <= -MaxStability / 2)
        {
            CurrentState -= StabilityTimer * Time.deltaTime;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                playerController.anim.enabled = true;
                Debug.Log("Launch");
            }
            //playerController.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerSolid2;
        }
        else if (CurrentState <= -MaxStability / 4)
        {
            Tete.sprite = TeteSolide;
            stabilityNeutre = false;
            stabilityGaseous = false;
            stabilitySolide = true;
            CurrentState -= StabilityTimer/2 * Time.deltaTime;
            audioSource.Stop();
            playerController.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerSolid;
            playerController.anim.enabled = false;
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

    public void GetHit(float value, EnemyManager.Type type)
    {
        switch(type)
        {
            case EnemyManager.Type.neutral:
                CurrentState += value;
                break;
            case EnemyManager.Type.solid:
                CurrentState -= value;
                break;
            case EnemyManager.Type.gaseous:
                CurrentState += value;
                break;
        }

        if (type == EnemyManager.Type.neutral)
        {
            CurrentState -= fireState;
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



