using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb2D;
    private Vector2 mousePosition;
    private Vector2 mouseDirection = new Vector2(1,0);
    private IAStateMachine _stateMachine;
    public int damage;
    [HideInInspector]
    public float life;
    public float maxLife;
    public StateManager stateManager;

    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    // Securite State
    private bool StateN = false;
    private bool StateG = false;
    private bool StateS = false;

    public Animator anim;

    private bool right = true;
    private bool up = true;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        _stateMachine = GetComponentInChildren<IAStateMachine>();
        Life1.SetActive(true);
        Life2.SetActive(true);
        Life3.SetActive(true);
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space) && _stateMachine._currentStateName != "SOLIDE")
        {
            _stateMachine._statesDico[_stateMachine._currentStateName].Shoot(this.transform.up);
        }

        if (stateManager.stabilityNeutre && !StateN)
        {
            Debug.Log("Neutre state");
            _stateMachine.SetState("NEUTRE");
            StateN = true;
            StateG = false;
            StateS = false;
        }

        if (stateManager.stabilityGaseous && !StateG)
        {
            Debug.Log("Gaseous state");
            _stateMachine.SetState("GAZEUX");
            StateN = false;
            StateG = true;
            StateS = false;
        }

        if (stateManager.stabilitySolide && !StateS)
        {
            Debug.Log("Solide state");
            _stateMachine.SetState("SOLIDE");
            StateN = false;
            StateG = false;
            StateS = true;
        }

        if (_stateMachine._currentStateName == "SOLIDE")
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            //float distance;
            //xy.Raycast(ray, out distance);
            //mousePosition = ray.GetPoint(distance);
            //mouseDirection = mousePosition - new Vector2(this.transform.position.x, this.transform.position.y);

            if (mouseDirection.y<=1 && right)
            {
                mouseDirection.y += 1 * Time.deltaTime;
                if (mouseDirection.y >= 1)
                {
                    right = false;
                }
            }
            else if(mouseDirection.y >=-1 && !right)
            {
                mouseDirection.y -= 1 * Time.deltaTime;
                if (mouseDirection.y <= -1)
                {
                    right = true;
                }
            }

            if (mouseDirection.x <= 1 && up)
            {
                mouseDirection.x += 1 * Time.deltaTime;
                if (mouseDirection.x >= 1)
                {
                    up = false;
                }
            }
            else if (mouseDirection.x >= -1 && !up)
            {
                mouseDirection.x -= 1 * Time.deltaTime;
                if (mouseDirection.x <= -1)
                {
                    up = true;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _stateMachine._statesDico[_stateMachine._currentStateName].Shoot(mouseDirection);
            }

            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            _stateMachine._statesDico[_stateMachine._currentStateName].GetComponent<IAStateSolide>().arrow.transform.position = transform.position + new Vector3(mouseDirection.normalized.x * 12, mouseDirection.normalized.y * 12, 0);
            _stateMachine._statesDico[_stateMachine._currentStateName].GetComponent<IAStateSolide>().arrow.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }

        if(life == 3)
        {
            Life1.SetActive(true);
            Life2.SetActive(true);
            Life3.SetActive(true);
        }

        if (life == 2)
        {
            Life1.SetActive(false);
            Life2.SetActive(true);
            Life3.SetActive(true);
        }
        if (life == 1)
        {
            Life1.SetActive(false);
            Life2.SetActive(false);
            Life3.SetActive(true);
        }
        if (life == 0)
        {
            Life1.SetActive(false);
            Life2.SetActive(false);
            Life3.SetActive(false);
            Death();
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_stateMachine._currentStateName == "SOLIDE")
        {
            _stateMachine._statesDico[_stateMachine._currentStateName].GetComponent<IAStateSolide>().touchWall = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_stateMachine._currentStateName == "SOLIDE" && collision.gameObject.tag == "Enemy" && _stateMachine._statesDico[_stateMachine._currentStateName].GetComponent<IAStateSolide>().canShoot)
        {
            collision.gameObject.GetComponent<EnemyManager>().hp -= damage;
        }
    }

    void GetDamage(float dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            Death();
        }
    }

    void Death()
    {

    }
}
