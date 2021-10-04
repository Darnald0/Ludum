using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffectY;
    private float  lengthY, startpos, startY;
    private GameObject cam;
    private float distY ;

    private void Start()
    {
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        startpos = transform.position.x;
        startY = transform.position.y;
        cam = Camera.main.gameObject;
        distY = cam.transform.position.y;
    }

    private void Update()
    {
        float tempY = (cam.transform.position.y);
        distY += (Time.deltaTime * parallaxEffectY);

        transform.position = new Vector3(startpos, distY, transform.position.z);

        if (distY > tempY + lengthY)
        {
            distY -= lengthY;
        }
        else if (distY < tempY - lengthY)
        {
            distY += lengthY;
        }
    }
}