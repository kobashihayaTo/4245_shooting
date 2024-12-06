using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    //体力
    [SerializeField]
    private float hp = 5;  //体力

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbodyを取得 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

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

    private void OnTriggerEnter(Collider collision)
    {

        //タグがEnemyBulletのオブジェクトが当たった時に{ }
        //内の処理が行われる
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("hit Enemy");  //コンソールにhit Playerが表示
            hp -= 1;
        }

        //体力が0以下になった時{}内の処理が行われる
        if (hp <= 0)
        {
            Destroy(gameObject);  //ゲームオブジェクトが破壊される
            SceneManager.LoadScene("GameOver");  // ゲームクリア画面に移行する
        }

    }
}
