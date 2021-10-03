using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb2D;
    private Vector2 mousePosition;
    private Vector2 mouseDirection;
    private IAStateMachine _stateMachine;
    public int damage;
    private float health;
    public float maxHealth;
    public StateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        _stateMachine = GetComponentInChildren<IAStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);
            mousePosition = ray.GetPoint(distance);
            ////////////////  For Camera in Orthographic /////////////////////////
            //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            /////////////////////////////////////////////////////////////////////
            mouseDirection = mousePosition - new Vector2(this.transform.position.x, this.transform.position.y);
            _stateMachine._statesDico[_stateMachine._currentStateName].Shoot(mouseDirection);
        }

        if (stateManager.stabilityNeutre)
        {
            Debug.Log("Neutre state");
            _stateMachine.SetState("NEUTRE");
        } 

        if (stateManager.stabilityGaseous)
        {
            Debug.Log("Gaseous state");
            _stateMachine.SetState("GAZEUX");
        }

        if (stateManager.stabilitySolide)
        {
            Debug.Log("Solide state");
            _stateMachine.SetState("SOLIDE");
        }

        if(_stateMachine._currentStateName == "SOLIDE")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);
            mousePosition = ray.GetPoint(distance);
            mouseDirection = mousePosition - new Vector2(this.transform.position.x, this.transform.position.y);

            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            _stateMachine._statesDico[_stateMachine._currentStateName].GetComponent<IAStateSolide>().arrow.transform.position = transform.position + new Vector3(mouseDirection.normalized.x * 6, mouseDirection.normalized.y * 6, 0);
            _stateMachine._statesDico[_stateMachine._currentStateName].GetComponent<IAStateSolide>().arrow.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }

    }

    void GetDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
