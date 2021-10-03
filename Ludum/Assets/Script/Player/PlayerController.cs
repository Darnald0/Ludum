using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;

    public int realSpeed;
    [HideInInspector]
    public Rigidbody2D rb2D;
    public GameObject bullet;
    private Vector2 mousePosition;
    private Vector2 mouseDirection;
    private IAStateMachine _stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        realSpeed = speed;
        rb2D = GetComponent<Rigidbody2D>();
        _stateMachine = GetComponentInChildren<IAStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
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
            Shoot();
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


    void Shoot()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(bullet,transform.position +new Vector3(mouseDirection.normalized.x*6, mouseDirection.normalized.y*6,0), Quaternion.Slerp(transform.rotation,rotation,1));
    }
}
