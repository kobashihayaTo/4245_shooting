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

    private bool flag = false;

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
        if(other.CompareTag("Player_ally"))
        {
            //近くのplayer_allyの方向を取得し続ける
            nearObj = SerchTag(gameObject, "Player_ally");
            //一番近くのplayer_allyの方向を向く
            transform.LookAt(nearObj.transform);

            if (SerchTag(gameObject, "Player_ally"))
            {
                Debug.Log("範囲に入りました");
                count++;
                //ここで数字を変えて弾の打つ感覚の変更
                if (count % 10 == 0)
                {
                    Instantiate(canonball, transform.position, RB.rotation);
                    //
                }
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

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("範囲から出ました");
        // transformを取得
        Transform myTransform = this.transform;
        // ワールド座標を基準に、回転を取得
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
        worldAngle.y = 180.0f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
        worldAngle.z = 0.0f; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
        myTransform.eulerAngles = worldAngle; // 回転角度を設定
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
