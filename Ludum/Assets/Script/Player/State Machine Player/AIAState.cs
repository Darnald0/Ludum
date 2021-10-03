using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIAState : MonoBehaviour
{
    protected IAStateMachine _stateMachine;
    public void InitState(IAStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void StateStart()
    {

    }

    public virtual void StateEnd()
    {

    }

    public abstract void StateUpdate();

    public abstract void Shoot(Vector2 mouseDirection);

    private void OnValidate()
    {
        string gameObjectName = BuildGameObjectName();
        if (gameObjectName != gameObject.name)
        {
            gameObject.name = gameObjectName;
        }
    }

    protected abstract string BuildGameObjectName();
}
