using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    private int realSpeed;
    private Rigidbody2D rb2D;
    public GameObject bullet;
    private Vector2 mousePosition;
    private Vector2 mouseDirection;

    // Start is called before the first frame update
    void Start()
    {
        realSpeed = speed;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            realSpeed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            realSpeed /= 2;
        }

        float hz = Input.GetAxis("Horizontal");
        float vrt = Input.GetAxis("Vertical");

        rb2D.velocity = new Vector2(hz * realSpeed, vrt * realSpeed) * Time.deltaTime;
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
    }


    void Shoot()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(bullet,transform.position +new Vector3(mouseDirection.normalized.x, mouseDirection.normalized.y,0), Quaternion.Slerp(transform.rotation,rotation,1));
    }
}
