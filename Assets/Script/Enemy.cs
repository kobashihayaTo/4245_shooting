using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Rigidbody RB;
    //敵の横速度
    public float moveHorizontal = 0f;
    //敵の縦速度
    public float moveVertical = 0f;
    //範囲に入ったら
    public float attackRange = 0.0f;
    public Transform Player_ally;
    public GameObject canonball;
    private int count = 0;
    //体力
    [SerializeField]
    private float hp = 5;  //体力
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        //自動で迫ってくる
        RB.velocity = transform.forward * moveVertical;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RB.velocity = -transform.right * moveHorizontal;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RB.velocity = transform.right * moveHorizontal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player_ally"))
        {

            transform.LookAt(Player_ally);
            count++;

            //ここで数字を変えて弾の打つ感覚の変更
            if (count % 10 == 0)
            {
                Instantiate(canonball, transform.position, RB.rotation);
                //Debug.Log("範囲に入りました");
            }

        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        //タグがEnemyBulletのオブジェクトが当たった時に{}内の処理が行われる
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("hit Player");  //コンソールにhit Playerが表示
            hp -= 1;
        }

        //体力が0以下になった時{}内の処理が行われる
        if (hp <= 0)
        {
            Destroy(gameObject);  //ゲームオブジェクトが破壊される
            //SceneManager.LoadScene("GameClear");  // ゲームクリア画面に移行する
        }
    }
}
