using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAStateMachine : MonoBehaviour
{
    protected AIAState _iaState;
    public string _currentStateName = "";
    public Dictionary<string, AIAState> _statesDico = new Dictionary<string, AIAState>();
    private bool start;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerController playerController;


    void Awake()
    {
        RegisterStates();
        player = this.gameObject.transform.parent.gameObject;
        playerController = player.GetComponent<PlayerController>();
    }

    void RegisterStates()
    {
        AIAState[] allStates = GetComponentsInChildren<AIAState>();

        foreach (AIAState state in allStates)
        {
            RegisterState(state.gameObject.name, state);
        }

        _currentStateName = allStates[0].gameObject.name;
        //allStates[0].StateStart();
    }

    void RegisterState(string name, AIAState state)
    {
        state.InitState(this);
        _statesDico[name] = state;
    }

    public void Update()
    {
        AIAState currentState = _statesDico[_currentStateName];
        if (!start)
        {
            currentState.StateStart();
            start = true;
        }
        currentState.StateUpdate();
    }

    public void SetState(string name)
    {
        AIAState currentState = _statesDico[_currentStateName];
        currentState.StateEnd();
        _currentStateName = name;
        AIAState nextState = _statesDico[_currentStateName];
        nextState.StateStart();
        start = true;
    }
}
