using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject Player_ally;
    public float Speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player_ally = GameObject.Find("Player_ally");
        //transform.LookAt(Player_ally.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Speed);
        Destroy(gameObject, 2);
    }
}
