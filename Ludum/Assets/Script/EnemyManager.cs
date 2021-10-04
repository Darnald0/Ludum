using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum Type { neutral, solid, gaseous };
    public enum Pattern { MarcheAvant, Diagonale, Stationnaire, ZigZag, Roue, ZigZagSharp};
    public float bulletCD = 1.0f;
    public float laserTickCD = 0.1f;
    public float speed = 2.0f;
    public int scoreValue = 100;
    public Type EnemyType;
    public Pattern pattern;

    public float diagonaleAngle;
    public float stationnaireDistance;
    public float stationnaireTime;
    public float zigZagRange;
    public float roueRadius;
    public float zigzagsharpRange;
    public float zigzagsharpTime;
    public float zigzagsharpDistance;

    public GameObject bulletPrefab;
    public GameObject droppedBonus;
    public Sprite neutral;
    public Sprite solid;
    public Sprite gazeous;
    public SpriteRenderer spriteRenderer;

    public bool disableBool;

    private Vector2 stationnaireTargetPos;
    private bool stationnaireStopped = false;

    private bool zigzagDir = false;
    public float zigzagFrequency;
    public float zigzagMagnitude;

    private Vector2 roueCenter;
    private float roueAngle;
    private bool zigzagSharpIsAtPos = true;
    private bool reverse = false;
    private Vector2 zigzagsharpNewPos;
    private float timer;
    private bool timerStop = false;
    private float waitCount;
    private float waitCD;
    private void Awake()
    {
        roueCenter = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        switch (EnemyType)
        {
            case Type.neutral:
                //spriteRenderer.sprite = neutral;
                break;
            case Type.solid:
                //spriteRenderer.sprite = solid;
                break;
            case Type.gaseous:
                //spriteRenderer.sprite = gazeous;
                break;
            default:
                Debug.Log("Error");
                break;
        }
        stationnaireTargetPos.x = transform.position.x;
        stationnaireTargetPos.y = transform.position.y - stationnaireDistance;
    }

    // Update is called once per frame
    private void Update()
    {
        timer = Time.deltaTime;
        Move();

        switch (EnemyType)
        {
            case Type.neutral:
                break;
            case Type.solid:
                break;
            case Type.gaseous:
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }

    private void Move()
    {
        switch(pattern)
        {
            case Pattern.MarcheAvant:
                transform.position = new Vector2(transform.position.x, transform.position.y - 1 * speed * Time.deltaTime);
                break;
            case Pattern.Diagonale:
                Vector2 dir = (Quaternion.Euler(0, 0, diagonaleAngle) * Vector2.right);
                transform.position = new Vector2(transform.position.x - dir.x * speed * Time.deltaTime, transform.position.y - dir.y * speed * Time.deltaTime);
                break;
            case Pattern.Stationnaire:
                transform.position = Vector2.MoveTowards(transform.position, stationnaireTargetPos, speed * Time.deltaTime);
                if (transform.position.y <= stationnaireTargetPos.y && !stationnaireStopped)
                {
                    Wait(stationnaireTime);
                }
                break;
            case Pattern.ZigZag:
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.position = transform.position + transform.right * Mathf.Sin(Time.time * zigzagFrequency) * zigzagMagnitude;
                break;
            case Pattern.Roue:
                roueAngle += speed * Time.deltaTime;
                var offset = new Vector2(Mathf.Sin(roueAngle), Mathf.Cos(roueAngle)) * roueRadius;
                transform.position = roueCenter + offset;
                break;
            case Pattern.ZigZagSharp:
                if (zigzagSharpIsAtPos && !timerStop)
                {
                    if (!reverse)
                    {
                        var x = zigzagsharpDistance * Mathf.Cos(zigzagsharpRange * Mathf.Deg2Rad);
                        var y = zigzagsharpDistance * Mathf.Sin(zigzagsharpRange * Mathf.Deg2Rad);
                        zigzagsharpNewPos = transform.position;
                        zigzagsharpNewPos.x -= x;
                        zigzagsharpNewPos.y -= y;
                        zigzagSharpIsAtPos = false;
                        Debug.Log(zigzagsharpNewPos);
                    }
                    else
                    {
                        var x = zigzagsharpDistance * Mathf.Cos(zigzagsharpRange * Mathf.Deg2Rad);
                        var y = zigzagsharpDistance * Mathf.Sin(zigzagsharpRange * Mathf.Deg2Rad);
                        zigzagsharpNewPos = transform.position;
                        zigzagsharpNewPos.x += x;
                        zigzagsharpNewPos.y -= y;
                        zigzagSharpIsAtPos = false;
                        Debug.Log(zigzagsharpNewPos);
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, zigzagsharpNewPos, speed * Time.deltaTime);

                if (transform.position.x == zigzagsharpNewPos.x && transform.position.y == zigzagsharpNewPos.y)
                {
                    Wait(zigzagsharpTime);
                }

                break;
            default:
                Debug.Log("Error");
                break;
        }

    }

    public void Wait(float waitTime)
    {
        if (!timerStop)
        {
            waitCount = waitTime;
            waitCD = bulletCD;
        }
        zigzagSharpIsAtPos = true;
        if (waitCount > 0f)
        {
            timerStop = true;
            Debug.Log(waitCount);
            waitCount -= timer;
            switch (EnemyType)
            {
                case Type.neutral:
                    waitCD -= timer;
                    if (waitCD < 0)
                    {
                        Debug.Log("shoot");
                        waitCD = bulletCD;
                    }
                    break;
                case Type.solid:
                    break;
                case Type.gaseous:
                    //Beam
                    break;
            }

        }
        else
        {
            if (pattern == Pattern.ZigZagSharp)
            {
                if (reverse)
                {
                    reverse = false;
                }
                else
                {
                    reverse = true;
                }
            }
            if (pattern == Pattern.Stationnaire)
            {
                stationnaireStopped = true;
                pattern = Pattern.MarcheAvant;
            }
            timerStop = false;
        }
    }

    public void OnDead(Type type)
    {
        GameObject bonus = Instantiate(droppedBonus);
        bonus.GetComponent<Bonus>().OnInstantiate(type);
        // Incremente score
    }
}
