using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject Player_ally;

    // Start is called before the first frame update
    void Start()
    {

        //Player_ally = GameObject.FindGameObjectWithTag("Player_ally");
        //transform.LookAt(Player_ally.transform);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, 0, 0.02f);
        Destroy(gameObject, 1.5f);
    }
}
