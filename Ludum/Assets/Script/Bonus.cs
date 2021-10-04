using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [HideInInspector] public EnemyManager.Type bonusType;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInstantiate(EnemyManager.Type type)
    {
        bonusType = type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // player get bonus
    }
}
