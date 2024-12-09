using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator = null;
    [SerializeField] private SceneSwitter scene;

    [SerializeField]
    private Transform player_pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbodyを取得 
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (player_pos.position.x >= -4.0f) 
        {
            Debug.Log("左　通ってる");
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f); // 値を設定
                                                               //transform.position -= speed * transform.right * Time.deltaTime;
            }
        }
        if(player_pos.position.x <= 4.0f)
        {
            Debug.Log("右　通ってる");
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector3(10.0f, 0.0f, 0.0f); // 値を設定
                                                              //transform.position += speed * transform.right * Time.deltaTime;
            }
            //rb.velocity.x = 3.0f;
        }

        // アニメーション用のフラグ管理
        if (scene.IsMode == true)
        {
            animator.SetBool("IsActive", true);
        }
        else
        {
            animator.SetBool("IsActive", false);
        }

        Debug.Log("velo:" + player_pos.position.x);

    }
}
