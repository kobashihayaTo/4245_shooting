using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 3.0f;

    private Animator animator = null;
    [SerializeField] private SceneSwitter scene;
    [SerializeField] private Transform player_pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); // rigidbodyを取得
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        // アニメーション用フラグ管理
        if (scene.IsMode == true)
        {
            animator.SetBool("IsActive", true);
        }
        else
        {
            animator.SetBool("IsActive", false);
        }

        if (player_pos.position.x >= -4.0f)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                Debug.Log("左　反応はしてる");
                transform.position -= speed * transform.right * Time.deltaTime;
            }
        }
        if (player_pos.position.x <= 4.0f)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                Debug.Log("右　反応はしてる");
                transform.position += speed * transform.right * Time.deltaTime;
            }
        }
        Debug.Log("velo:" + player_pos.position.x);
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
