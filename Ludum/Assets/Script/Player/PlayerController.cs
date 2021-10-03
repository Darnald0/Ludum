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

        if (Input.GetKeyDown(KeyCode.G))
        {
            _stateMachine.SetState("NEUTRE");
        } 

        if (Input.GetKeyDown(KeyCode.H))
        {
            _stateMachine.SetState("GAZEUX");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            _stateMachine.SetState("SOLIDE");
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
