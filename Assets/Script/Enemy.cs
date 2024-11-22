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
    //一番近いオブジェクト
    private GameObject nearObj;
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
        if (GameObject.FindGameObjectWithTag("Player_ally"))
        {
            //近くのplayer_allyの方向を取得し続ける
            nearObj = SerchTag(gameObject, "Player_ally");
        }

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
        //一番近くのplayer_allyの方向を向く
        transform.LookAt(nearObj.transform);
        if (SerchTag(gameObject,"Player_ally" ))
        {
           
            count++;

            //ここで数字を変えて弾の打つ感覚の変更
            if (count % 600 == 0)
            {
                Instantiate(canonball, transform.position, RB.rotation);
                //Debug.Log("範囲に入りました");
            }

        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        //タグがEnemyBulletのオブジェクトが当たった時に{ }
        //内の処理が行われる
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

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player_ally"))
        {
            Debug.Log("範囲から出ました");
        }
    }

    //指定されたタグの中で最も近いものを取得
    GameObject SerchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
}
