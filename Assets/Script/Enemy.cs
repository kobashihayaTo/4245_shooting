using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbodyを取得 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 10.0f); // 値を設定
                                                          //transform.position += speed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0.0f, 0.0f, -10.0f); // 値を設定
                                                           //transform.position -= speed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f); // 値を設定
                                                           //transform.position -= speed * transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(10.0f, 0.0f, 0.0f); // 値を設定
                                                          //transform.position += speed * transform.right * Time.deltaTime;
        }

    }
}
