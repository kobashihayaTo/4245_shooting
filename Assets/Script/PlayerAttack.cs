using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject enemy;
    public float Speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        transform.LookAt(enemy.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Speed);
        Destroy(gameObject, 2);
    }
}
