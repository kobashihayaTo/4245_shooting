using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlly : MonoBehaviour
{
    public float attackRange = 0.0f;
    public Transform Enemy;
    public GameObject canonball;
    private int count = 0;
    private SceneSwitter sceneSwitter;
    [SerializeField] private float hp = 5;// 体力

    // Start is called before the first frame update
    void Start()
    {
        // 当たり判定を取ってそれを攻撃範囲にする
        GetComponent<SphereCollider>().radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        // 当たり判定の可視化
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerStay(Collider other)
    {
        // Enemyタグを検索して処理を実行する
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("範囲に入った");
            transform.LookAt(Enemy);
            count++;

            // ここで数字を変えて弾の打つ間隔の変更
            if (count % 10 == 0)
            {
                Instantiate(canonball, transform.position, Quaternion.identity);
                Debug.Log("撃ってるよ");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("範囲から出ました");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // タグがEnemyBulletのオブジェクトが当たった時に{}内の処理が行われる
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("hit Player");// コンソールにhit Playerが表示
            hp--;
        }

        // 体力が0以下になった時{}内の処理が行われる
        if (hp <= 0)
        {
            Destroy(gameObject);// ゲームオブジェクトが破壊される
        }
    }
}