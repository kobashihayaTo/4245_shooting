using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator = null;
    [SerializeField] private SceneSwitter scene;
    [SerializeField] private Transform player_pos;

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
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {

                Debug.Log("左　反応はしてる");
                rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f); // 
                                                               //transform.position -= speed * transform.right * Time.deltaTime;
            }
        }
        if (player_pos.position.x <= 4.0f)
        {

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {

                Debug.Log("右　反応はしてる");
                rb.velocity = new Vector3(10.0f, 0.0f, 0.0f); // 
                                                              //transform.position += speed * transform.right * Time.deltaTime;
            }
            //rb.velocity.x = 3.0f;
        }

        Debug.Log("velo:" + player_pos.position.x);

        // アニメーション用フラグ管理
        if (scene.IsMode == true)
        {
            animator.SetBool("IsActive", true);
        }
        else
        {
            animator.SetBool("IsActive", false);
        }
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
    }

    private void OnTriggerEnter(Collider collision)
    {
        // タグがEnemyBulletのオブジェクトが当たった時に{ }
        // 内の処理が行われる
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("GameOver");  // ゲームクリア画面に移行する
        }
    }
}
