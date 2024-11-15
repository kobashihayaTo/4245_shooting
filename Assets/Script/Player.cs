using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbody‚ğæ“¾ 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f); // ’l‚ğİ’è
                                                           //transform.position -= speed * transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(10.0f, 0.0f, 0.0f); // ’l‚ğİ’è
                                                          //transform.position += speed * transform.right * Time.deltaTime;
        }

    }
}
